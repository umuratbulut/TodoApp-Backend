using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using TodoApp.DAL.Abstract;
using TodoApp.Entities.Concrete;
using TodoApp.Entities.DTOs;

namespace TodoApp.DAL.Concrete.EntityFramework
{
    public class EfTodoDal : EfEntityRepositoryBase<Todo, TodoAppContext>, ITodoDal
    {
        public void AddWithSendMail(Todo todo)
        {
            using (TodoAppContext context = new TodoAppContext())
            {

                var entity = context.Entry(todo);
                entity.State = EntityState.Added;
                context.SaveChanges();

                var user = context.Users.FirstOrDefault(x => x.ID.Equals(todo.UserID));

                MailMessage msg = new MailMessage(); //Mesaj gövdesini tanımlıyoruz...
                msg.Subject = "Todo's App Task Assignment";
                msg.From = new MailAddress("todoapptr@gmail.com", "Todo App");
                msg.To.Add(new MailAddress(user.Email, user.Name + " " + user.Surname));

                //Mesaj içeriğinde HTML karakterler yer alıyor ise aşağıdaki alan TRUE olarak gönderilmeli ki HTML olarak yorumlansın. Yoksa düz yazı olarak gönderilir...
                msg.IsBodyHtml = true;
                msg.Body = "Dear " + todo.User.Name + " " + todo.User.Surname + "," + "<p>Task <b>" + todo.Description + "</b> has been assigned to you.</p>";

                //Mesaj önceliği (BELİRTMEK ZORUNLU DEĞİL!)
                msg.Priority = MailPriority.Normal;


                //SMTP/Gönderici bilgilerinin yer aldığı erişim/doğrulama bilgileri
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587); //Bu alanda gönderim yapacak hizmetin smtp adresini ve size verilen portu girmelisiniz.
                NetworkCredential AccountInfo = new NetworkCredential("todoapptr@gmail.com", "manlok123");
                smtp.UseDefaultCredentials = false; //Standart doğrulama kullanılsın mı? -> Yalnızca gönderici özellikle istiyor ise TRUE işaretlenir.
                smtp.Credentials = AccountInfo;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.EnableSsl = true; //SSL kullanılarak mı gönderilsin...
                smtp.Send(msg);
            }
        }

        public List<GetTodoDetail> GetTodoDetail()
        {
            using (TodoAppContext context = new TodoAppContext())
            {

                var result = (from t in context.Todos
                              join u in context.Users
                              on t.UserID equals u.ID
                              select new GetTodoDetail
                              {
                                  TodoID = t.ID,
                                  TodoUserID = u.ID,
                                  UserName = u.Name + " " + u.Surname,
                                  UserNickname = u.Nickname,
                                  TodoDescription = t.Description,
                                  TodoDate = t.CreationDate.ToShortDateString() + " " + t.CreationDate.ToShortTimeString(),
                                  CreationDate = t.CreationDate,
                                  TodoStatus = t.isActive
                              });
                return result.ToList();

            }
        }

        public void SetStatus(int id)
        {
            using (TodoAppContext context = new TodoAppContext())
            {
                var entity = context.Todos.FirstOrDefault(x => x.ID.Equals(id));
                if (entity.isActive)
                    entity.isActive = false;
                else
                    entity.isActive = true;
                context.SaveChanges();
            }
        }

        public void UpdateWithSendMail(Todo todo)
        {
            using (TodoAppContext context1 = new TodoAppContext())
            {
                var btodo = context1.Todos.FirstOrDefault(x=>x.ID.Equals(todo.ID));
                using (TodoAppContext context = new TodoAppContext())
                {

                    var entity = context.Entry(todo);
                    entity.State = EntityState.Modified;
                    context.SaveChanges();


                    if (btodo.Description != todo.Description || btodo.UserID != todo.UserID)
                    {

                        var user = context.Users.FirstOrDefault(x => x.ID.Equals(todo.UserID));

                        MailMessage msg = new MailMessage(); //Mesaj gövdesini tanımlıyoruz...
                        msg.Subject = "Todo's App Task Assignment";
                        msg.From = new MailAddress("todoapptr@gmail.com", "Todo App");
                        msg.To.Add(new MailAddress(user.Email, user.Name + " " + user.Surname));

                        //Mesaj içeriğinde HTML karakterler yer alıyor ise aşağıdaki alan TRUE olarak gönderilmeli ki HTML olarak yorumlansın. Yoksa düz yazı olarak gönderilir...
                        msg.IsBodyHtml = true;
                        msg.Body = "Dear " + todo.User.Name + " " + todo.User.Surname + "," + "<p>Task <b>" + todo.Description + "</b> has been assigned to you.</p>";

                        //Mesaj önceliği (BELİRTMEK ZORUNLU DEĞİL!)
                        msg.Priority = MailPriority.High;


                        //SMTP/Gönderici bilgilerinin yer aldığı erişim/doğrulama bilgileri
                        SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587); //Bu alanda gönderim yapacak hizmetin smtp adresini ve size verilen portu girmelisiniz.
                        NetworkCredential AccountInfo = new NetworkCredential("todoapptr@gmail.com", "manlok123");
                        smtp.UseDefaultCredentials = false; //Standart doğrulama kullanılsın mı? -> Yalnızca gönderici özellikle istiyor ise TRUE işaretlenir.
                        smtp.Credentials = AccountInfo;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.EnableSsl = true; //SSL kullanılarak mı gönderilsin...
                        smtp.Send(msg);

                        if (btodo.UserID != todo.UserID)
                        {
                            var bUser = context1.Users.FirstOrDefault(x=>x.ID.Equals(btodo.UserID));

                            MailMessage msg1 = new MailMessage(); //Mesaj gövdesini tanımlıyoruz...
                            msg1.Subject = "Todo's App Mission Cancellation";
                            msg1.From = new MailAddress("todoapptr@gmail.com", "Todo App");
                            msg1.To.Add(new MailAddress(bUser.Email, bUser.Name + " " + bUser.Surname));

                            //Mesaj içeriğinde HTML karakterler yer alıyor ise aşağıdaki alan TRUE olarak gönderilmeli ki HTML olarak yorumlansın. Yoksa düz yazı olarak gönderilir...
                            msg1.IsBodyHtml = true;
                            msg1.Body = "Dear " + bUser.Name + " " + bUser.Surname + "," + "<p>Task <b>" + todo.Description + "</b> canceled!</p> <p><u>Reason for cancellation:</u> Task assigned to another user.</p>";

                            //Mesaj önceliği (BELİRTMEK ZORUNLU DEĞİL!)
                            msg1.Priority = MailPriority.High;


                            //SMTP/Gönderici bilgilerinin yer aldığı erişim/doğrulama bilgileri
                            SmtpClient smtp1 = new SmtpClient("smtp.gmail.com", 587); //Bu alanda gönderim yapacak hizmetin smtp adresini ve size verilen portu girmelisiniz.
                            NetworkCredential AccountInfo1 = new NetworkCredential("todoapptr@gmail.com", "manlok123");
                            smtp1.UseDefaultCredentials = false; //Standart doğrulama kullanılsın mı? -> Yalnızca gönderici özellikle istiyor ise TRUE işaretlenir.
                            smtp1.Credentials = AccountInfo1;
                            smtp1.DeliveryMethod = SmtpDeliveryMethod.Network;
                            smtp1.EnableSsl = true; //SSL kullanılarak mı gönderilsin...
                            smtp1.Send(msg1);

                        }
                    }
                }
            }
        }
    }
}
