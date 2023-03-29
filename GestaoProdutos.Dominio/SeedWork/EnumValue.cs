using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GestaoProduto.Dominio
{
    public class EnumValue
    {
        public int valor { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }

        public static List<EnumValue> GetValues<T>()
        {
            List<EnumValue> values = new List<EnumValue>();
            foreach (var itemType in Enum.GetValues(typeof(T)))
                values.Add(new EnumValue
                {
                    valor = (int)itemType,
                    nome = Enum.GetName(typeof(T), itemType)
                });

            return values;
        }

        public static List<EnumValue> GetDescriptions<T>()
        {
            List<EnumValue> values = new List<EnumValue>();
            foreach (var itemType in Enum.GetValues(typeof(T)))
            {
                values.Add(new EnumValue
                {
                    valor = (int)itemType,
                    nome = Enum.GetName(typeof(T), itemType),
                    descricao = ObterDescricaoEnum<T>(itemType)
                });
            }

            return values;
        }

        private static string ObterDescricaoEnum<T>(object tipo)
        {
            var type = typeof(T);
            var memInfo = type.GetMember(tipo.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            var descricao = ((DescriptionAttribute)attributes[0]).Description;
            return descricao;
        }
    }
}
