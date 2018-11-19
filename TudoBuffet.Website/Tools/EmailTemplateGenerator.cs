﻿namespace TudoBuffet.Website.Tools
{
    public class EmailTemplateGenerator
    {
        public static string GetEmailConfirmationTemplate(string link)
        {
            string emailConfirmationTemplate = @"<html>
                                                    <head></head>
                                                    <body>
                                                        Você está recebendo esse e-mail da equipe TudoBuffet.
                                                        <br>
                                                        Para confirmar o cadastro, clique ou copie e cole o link abaixo:
                                                        <br>
                                                        #link#
                                                        <br>
                                                        Obrigado
                                                        <br>
                                                        TudoBuffet
                                                    </body>
                                                    </html>";

            return emailConfirmationTemplate.Replace("#link#", link);
        }
    }
}
