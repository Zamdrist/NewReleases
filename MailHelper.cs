using System;
using System.Net;
using System.Net.Mail;

namespace NewReleases
{
	public class MailHelper : IDisposable
	{
		//using GMail as smtp relay host
		private SmtpClient smtpClient = new SmtpClient();
		private MailMessage message = new MailMessage();
		private const int Port = 587;
		private const string Host = "smtp.gmail.com";
		private const bool EnableSsl = true;
		private const bool UseDefaultCredentials = true;
		private const bool IsBodyHtml = true;
		private string _subject;
		private string _body;

		public MailHelper(MailAddress from, string credentials, string subject, string body)
		{
			this.smtpClient.Port = MailHelper.Port;
			this.smtpClient.Host = MailHelper.Host;
			this.smtpClient.EnableSsl = MailHelper.EnableSsl;
			this.smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
			this.smtpClient.UseDefaultCredentials = MailHelper.UseDefaultCredentials;
			this.smtpClient.Credentials = new NetworkCredential(from.Address, credentials);
			this.message.From = from;
			this._subject = subject;
			this._body = body;
		}

		public void Dispose()
		{

		}

		public void Send(MailAddress to)
		{
			this.message.To.Add(to);
			this.message.IsBodyHtml = MailHelper.IsBodyHtml;
			this.message.Subject = this._subject;
			this.message.Body = this._body;
			this.smtpClient.Send(this.message);
		}
	}
}
