using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VTKeygen
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var key = GenerateKey();
            textBox1.Text = key.Substring(0, 4);
            textBox2.Text = key.Substring(4, 4);
            textBox3.Text = key.Substring(8, 4);
            textBox4.Text = key.Substring(12, 4);
        }

        private static Random random = new Random();

        public static string GenerateKey()
        {
            char[] key = new char[16];

            key[0] = RandomAlphanumeric();
            key[1] = RandomAlphanumeric();

            key[2] = (((byte)key[0] + (byte)key[1]) & 1) == 0 ? 's' : 'j';

            key[3] = RandomAlphanumeric();
            
            var threeCheck = random.Next(10);
            if (IsSumEven(key[0], key[3]))
            {
                if(threeCheck < 5) threeCheck += 5;
            }
            else
            {
                if (threeCheck >= 5) threeCheck -= 5;
            }
            key[5] = (char)((byte)'0' + threeCheck);

            key[4] = RandomAlphanumeric();

            var fourCheck = random.Next(10);
            if (IsSumEven(key[1], key[4]))
            {
                if (threeCheck >= 5) threeCheck -= 5;
            }
            else
            {
                if (threeCheck < 5) threeCheck += 5;
            }
            key[6] = (char)((byte)'0' + fourCheck);
          
            key[7] = RandomAlphanumeric();
            key[8] = RandomLowercaseLetter();

            if (key[2] != 'j')
            {
                // funny way to swap values
                key[7] ^= key[8];
                key[8] ^= key[7];
                key[7] ^= key[8];
            }

            key[9] = RandomAlphanumeric();
            key[10] = RandomAlphanumeric();

            key[11] = (char)(((key[0] + key[1]) % 0x1A) + 'a');
            key[12] = (char)(((key[2] + key[3]) % 0x1A) + 'a');
            key[13] = (char)(((key[0] + key[2]) % 0x1A) + 'a');
            key[14] = (char)(((key[3] + key[1]) % 0x1A) + 'a');

            key[15] = key[2] == 'j' ? GetRandomCharacter("ankich") : GetRandomCharacter("kimjno");

            return new string(key);
        }

        private static bool IsSumEven(char lhs, char rhs)
        {
            return (((byte)lhs + (byte)rhs) & 1) == 0;
        }

        private static char RandomLowercaseLetter()
        {
            return (char)random.Next('a', 'z' + 1);
        }

        private static char RandomAlphanumeric()
        {
            switch(random.Next(3))
            {
                case 1:
                    return (char)random.Next('A', 'Z' + 1);
                case 2:
                    return (char)random.Next('a', 'z' + 1);
                default:
                case 0:
                    return (char)random.Next('0', '9' + 1);
            }
        }

        private static char GetRandomCharacter(string chars)
        {
            return chars[random.Next(chars.Length)];
        }
    }
}
