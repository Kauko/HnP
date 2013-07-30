using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RuttoPuput
{
    enum CritterType { male, female, doctor, plague };

    class CritterManager
    {
        private Random rand = new Random();

        private List<Critter> mFemales;
        private List<Critter> mMales;
        private List<Critter> mDoctors;
        private List<Critter> mPlagues;

        public void Initialize()
        {
            mFemales = new List<Critter>();
            mMales = new List<Critter>();
            mDoctors = new List<Critter>();
            mPlagues = new List<Critter>();
        }

        public Critter SpawnRandomCritter()
        {
            int i = rand.Next(C.genderDistribution.Length);

            return SpawnCritter(C.genderDistribution[i], 0);
        }


        public Critter SpawnCritter(char cType, int age)
        {
            Critter critter; 

            if (cType == 'M')
            {
                critter = new Male(TextureRefs.maleTexture, new Rectangle(), age);
                mMales.Add(critter);
            }
            else if (cType == 'F')
            {
                critter = new Female(TextureRefs.femaleTexture, new Rectangle(), age);
                mFemales.Add(critter);
            }
            else if (cType == 'D')
            {
                critter = new Doctor(TextureRefs.doctorTexture, new Rectangle(), age);
                mDoctors.Add(critter);
            }
            else
            {
                critter = new Plagued(TextureRefs.plagueTexture, new Rectangle(), age);
                mPlagues.Add(critter);
            }

            return critter;
        }


        public Plagued SpawnPlagueCritter()
        {
            Plagued p = new Plagued(TextureRefs.plagueTexture, new Rectangle(), 0);
            mPlagues.Add(p);
            return p;
        }

        public void RemovePlagued(Critter c)
        {
            string ct = c.GetType().Name;
            if (ct == "Female")
            {
                mFemales.Remove(c);
            }
            if (ct == "Doctor")
            {
                mDoctors.Remove(c);
            }
            if (ct == "Male")
            {
                mMales.Remove(c);
            }
        }

        public bool ManageCritters()
        {
            ContaminateCritters();
            BreedCritters();
            AgeCritters();

            if (mFemales.Count == 0 || mMales.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ContaminateCritters()
        {
            foreach (Plagued p in mPlagues.GetRange(0, mPlagues.Count))
            {
                p.SpreadPlague();
            }
        }

        public void AgeCritters()
        {

            foreach (Female f in mFemales.GetRange(0, mFemales.Count))
            {
                if (f.GetAge() == C.death)
                {
                    f.RemoveFromGrid();
                    mFemales.Remove(f);
                }
                f.IncreaseAge();
            }

            foreach (Male m in mMales.GetRange(0, mMales.Count))
            {
                if (m.GetAge() == C.death)
                {
                    m.RemoveFromGrid();
                    mMales.Remove(m);
                }
                m.IncreaseAge();
            }
            foreach (Doctor d in mDoctors.GetRange(0, mDoctors.Count))
            {
                if (d.GetAge() == C.death)
                {
                    d.RemoveFromGrid();
                    mDoctors.Remove(d);
                }
                d.IncreaseAge();
            }

        }

        public void BreedCritters()
        {
            foreach (Female f in mFemales.GetRange(0, mFemales.Count))
            {
                if(f.IsFertile()) f.CheckMates();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Female f in mFemales)
            {
                f.Draw(spriteBatch);
            }

            foreach (Male m in mMales)
            {
                m.Draw(spriteBatch);
            }

            foreach (Doctor d in mDoctors)
            {
                d.Draw(spriteBatch);
            }

            foreach (Plagued p in mPlagues)
            {
                p.Draw(spriteBatch);
            }
        }


    }
}
