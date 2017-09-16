using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pando
{
    public partial class Form1 : Form
    {
        List<Location> locations = new List<Location>();
        double c1;
        double c2;
        int numberOfIterations;
        int numberOfParticles;
        public Form1()
        {
            InitializeComponent();
            for (int i = 1; i <= 100; ++i)
            {         
                String labelname = "label" + i;
                Control ct = this.Controls[labelname];
                ct.Click += label1_Click;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBoxC1.Text += "2";
            textBoxC2.Text += "2";
            textBoxN.Text += "10";
            textBoxPart.Text += "20";

        }

        private void label1_Click(object sender, EventArgs e)
        {
            (sender as Label).BackColor = Color.FromArgb(255, 0, 0);
           
            String labelName = (sender as Label).Name;
            char[] separatingChars = { 'l','a','b','e' };
            String[] coords = labelName.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);
            int i = Int32.Parse(coords[0]);
            int x = (i - 1) / 10; //linie: (i-1)/10
            int y = (i - 1) % 10;  //coloana: (i-1)%10
            textBoxCoords.Text = "["+x+", "+y+"]";

            foreach (var lbl in Controls.OfType<Label>())
                lbl.Enabled=false;
                
            //Double p = Double.Parse(textBoxP.Text);
           // String locationName = textBoxName.Text;
           // locations.Add(new Location(x, y, p, locationName));
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            Double p = Double.Parse(textBoxP.Text);
            String locationName = textBoxName.Text;
            char[] separatingChars = { '[',']',' ',',' };
            String[] coords = textBoxCoords.Text.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);

            int x = Int32.Parse(coords[0]);  //linie
            int y = Int32.Parse(coords[1]);  //coloana
            Location l = new Location(x, y, p, locationName);
            textBoxStatus.Text += l.GetInfo()+"\r\n";
            
            bool exists = false;
            foreach (var loc in locations)
            {
                if (loc.x == l.x && loc.y == l.y)
                {
                    exists = true;
                    loc.p = l.p;
                    loc.name = l.name;
                    textBoxStatus.Text += "Locatia exista! detalii schimbate." + "\r\n";
                }

            }
            if (!exists)
            {
                locations.Add(l);
                textBoxStatus.Text += "Locatie adaugata!" + "\r\n";
            }
           
            foreach (var lbl in Controls.OfType<Label>())
                lbl.Enabled = true;

            textBoxName.Text = "";
            textBoxP.Text = "";
            textBoxCoords.Text = "";

        }

        private Particle ParticleSwarm(List<Location> locations, int numberOfParticles, int numberOfIterations,
            double c1, double c2)
        {

            Particle pBest = new Particle(0, 0, Double.MaxValue);
            List<Particle> particles = new List<Particle>();
            for (int part = 0; part < numberOfParticles; ++part)
            {
                particles.Add(new Particle());
            }

            for (int iter = 1; iter <= numberOfIterations; ++iter)
            {
                Particle gBest = new Particle(0, 0, Double.MaxValue);
                foreach (var particle in particles)
                {
                    particle.computeFitness(locations);
                    if (particle.fitness < pBest.fitness)
                    {
                        pBest.x = particle.x;
                        pBest.y = particle.y;
                        pBest.fitness = particle.fitness;
                    }
                    if (particle.fitness < gBest.fitness)
                    {
                        gBest.x = particle.x;
                        gBest.y = particle.y;
                        gBest.fitness = particle.fitness;
                    }

                }


                foreach (var particle in particles)
                {
                    particle.computeVelocity(pBest, gBest, c1, c2);
                    particle.x += particle.vx;
                    particle.y += particle.vy;
                }
            }
            return pBest;
        }

        private void buttonGo_Click_1(object sender, EventArgs e)
        {
            c1 = Double.Parse(textBoxC1.Text);
            c2 = Double.Parse(textBoxC2.Text);
            numberOfIterations = Int32.Parse(textBoxN.Text);
            numberOfParticles = Int32.Parse(textBoxPart.Text);
            textBoxStatus.Text += "\r\n\r\n";
            double totalSales = 0;
            foreach (var loc in locations)
            {
                totalSales += loc.p;
                textBoxStatus.Text += loc.GetInfo() + "\r\n";
            }
            textBoxStatus.Text += "\r\n\r\n";
            foreach (var loc in locations)
            {
                loc.p = loc.p / totalSales;
            }

            //TODO de aplelat functia care calculeaza Particle Swarm
            //Particle wanted;

            Particle res = ParticleSwarm(locations, numberOfParticles, numberOfIterations, c1, c2);
            int x = Convert.ToInt32(res.x);
            int y = Convert.ToInt32(res.y);
            string name = string.Format("label{0}", x*10+(y+1));
            Label  winner = this.Controls.Find(name, true)[0] as Label;
            winner.BackColor = Color.FromArgb(0, 255, 0);
            Location winnerLocation = new Location(x, y, 1, "Centrul de Distributie cautat");
            textBoxStatus.Text += winnerLocation.GetInfo() + "\r\n";

        }


    }
}
