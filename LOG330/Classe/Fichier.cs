﻿using System;
using System.Collections.Generic;
using System.IO;
//test
namespace LOG330.Classe
{
    public static class Fichier
    {
        public static string[] Lire(string path)
        {
            List<double> listeNombre  = new List<double>();
            String[] lignesFichier;       

            try
            {
                 lignesFichier = File.ReadAllLines(path);
            }
            catch (Exception e)
            {
                throw e;
            }

            return lignesFichier;
        }
    }
}