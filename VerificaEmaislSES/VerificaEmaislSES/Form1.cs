using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Amazon;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;

namespace VerificaEmailsSES
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string awsAccessKeyId = ""; //informar credencial


        string awsSecretAccessKey = ""; //informar credencial

        //cadastra o e-mail
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (var client = new AmazonSimpleEmailServiceClient(awsAccessKeyId, awsSecretAccessKey, RegionEndpoint.SAEast1))
                {

                    var response = client.VerifyEmailIdentity(new VerifyEmailIdentityRequest

                    {
                        EmailAddress = textBox1.Text

                    });

                    label1.Text = "";
                    label1.Text = "Registro efetuado com sucesso!";
                    label1.Visible = true;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Ocorreu um problema, verifique!: " + err.Message);
            }
        }

        //verifica o status do e-mail na aws
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (var client = new AmazonSimpleEmailServiceClient(awsAccessKeyId, awsSecretAccessKey, RegionEndpoint.SAEast1))
                {

                    var response = client.GetIdentityVerificationAttributes(new GetIdentityVerificationAttributesRequest
                    {
                        Identities = new List<string> {

                            textBox1.Text
                        }
                    });

                    Dictionary<string, IdentityVerificationAttributes> verificationAttributes = response.VerificationAttributes;

                    foreach (var i in verificationAttributes)
                    {
                        label1.Text = "";
                        label1.Text = "Status do E-mail: " + i.Value.VerificationStatus;
                        label1.Visible = true;
                    }


                }
            }
            catch (Exception err)
            {

                MessageBox.Show("Ocorreu um problema, verifique!: " + err.Message);
            }
        }

        //remove o e-mail cadastrado
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                using (var client = new AmazonSimpleEmailServiceClient(awsAccessKeyId, awsSecretAccessKey, RegionEndpoint.SAEast1))
                {

                    var response = client.DeleteIdentity(new DeleteIdentityRequest
                    {
                        Identity = textBox1.Text

                    });

                }

                label1.Text = "";
                label1.Text = "E-mail removido com sucesso!";
                label1.Visible = true;

            }
            catch (Exception err)
            {

                MessageBox.Show("Ocorreu um problema, verifique!: " + err.Message);
            }
        }

    }
}
