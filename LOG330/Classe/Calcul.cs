﻿using System;
using System.Collections.Generic;

namespace LOG330.Classe
{
    public static class Calcul
    {
        const int minNombreListe = 3;
        
        public static double CalculerMoyenne(List<double> listeNombres)
        {
            if (listeNombres.Count == 0)
            {
                return 0;
            }
            
            double somme = 0;

            for (int i = 0; i < listeNombres.Count; i++)
            {
                somme += listeNombres[i];
            }

            return somme / listeNombres.Count;
        }

        public static double CalculerVariance(List<double> listeNombres, double nbNombre)
        {
            if (listeNombres.Count == 0 || listeNombres.Count != nbNombre)
            {
                return 0;
            }
            
            double moyenne = CalculerMoyenne(listeNombres);
            double sommeDistance = 0;
    
            for (int i = 0; i < listeNombres.Count; i++)
            {
                sommeDistance += ((listeNombres[i] - moyenne) * (listeNombres[i] - moyenne));
            }

            return ((1 / (nbNombre - 1)) * sommeDistance);
        }

        public static double CalculerEcartType(List<double> listeNombres, double nbNombre)
        {
            if (listeNombres.Count == 0 || listeNombres.Count != nbNombre)
            {
                return 0;
            }
            
            double variance = CalculerVariance(listeNombres, nbNombre);

           return Math.Sqrt(variance);
        }

        public static double CalculerSomme(List<double> listeNombres)
        {
            if (listeNombres.Count == 0)
            {
                return 0;
            }
            
            double somme = 0;

            for (int i = 0; i < listeNombres.Count; i++)
            {
                somme += listeNombres[i]; 
            }

            return somme;
        }

        public static double CalculerCorrelation(List<double> listeX, List<double> listeY, double nbNombre)
        {
            if (listeX.Count < minNombreListe || listeY.Count < minNombreListe || nbNombre != listeX.Count || 
                nbNombre != listeY.Count)
            {
                return 0;
            }
            
            List<double> listeXX = new List<double>();
            List<double> listeYY = new List<double>();
            List<double> listeXY = new List<double>();

            for (int i = 0; i < nbNombre; i++)
            {
                listeXX.Add(listeX[i] * listeX[i]);
                listeYY.Add(listeY[i] * listeY[i]);
                listeXY.Add(listeY[i] * listeX[i]);
            }

            double sommeX = CalculerSomme(listeX);
            double sommeY = CalculerSomme(listeY);
            double sommeXX = CalculerSomme(listeXX);
            double sommeYY = CalculerSomme(listeYY);
            double sommeXY = CalculerSomme(listeXY);

            return (((nbNombre * sommeXY) - (sommeX * sommeY)) / 
            (Math.Sqrt(((nbNombre * sommeXX) - (sommeX * sommeX)) * ((nbNombre * sommeYY) - (sommeY * sommeY))))); 
        }

        public static double CalculerPenteRegression(List<double> listeX, List<double> listeY, double nbNombre)
        {
            if (listeX.Count < minNombreListe || listeY.Count < minNombreListe || nbNombre != listeX.Count || 
                nbNombre != listeY.Count)
                
            {
                return 0;
            }
            
            List<double> listeXX = new List<double>();
            List<double> listeXY = new List<double>();
            
            for (int i = 0; i < nbNombre; i++)
            {
                listeXX.Add(listeX[i] * listeX[i]);
                listeXY.Add(listeY[i] * listeX[i]);
            }

            double sommeX = CalculerSomme(listeX);
            double sommeY = CalculerSomme(listeY);
            double sommeXX = CalculerSomme(listeXX);     
            double sommeXY = CalculerSomme(listeXY);

            return (((nbNombre * sommeXY) - (sommeX * sommeY)) / ((nbNombre * sommeXX) - (sommeX * sommeX))); 
        }
        
        public static  double CalculerConstanteRegression(List<double> listeX, List<double> listeY, double pente)
        {
            if (listeX.Count < minNombreListe || listeY.Count < minNombreListe)
            {
                return 0;
            }
            
            double moyenneX = CalculerMoyenne(listeX);
            double moyenneY = CalculerMoyenne(listeY);

            return moyenneY - pente * moyenneX;
        }

        public static double CalculerEcartTypeListe(List<double> listX, List<double> listY, double nbDonnee)
        {
            double sommeYEcart = 0;
            double pente = CalculerPenteRegression(listX, listY, nbDonnee);
            double constante = CalculerConstanteRegression(listX, listY, pente);


            for (int i = 0; i < nbDonnee; i++)
            {
                double yReg = 0;

                yReg = pente * listX[i] + constante;

                sommeYEcart += (listY[i] - yReg) * (listY[i] - yReg);
            }

            return Math.Sqrt((1 / (nbDonnee - 1)) * sommeYEcart);
        }


        public static double CalculerIntervalleConfiance(double ecartType, double donneeEstime, double valeurStudent,
            List<double> listX, double nbDonnee)
        {
            double sommeXMoyen = 0;
            double moyenneListX = CalculerMoyenne(listX);

            for (int i = 0; i < nbDonnee; i++)
            {
                sommeXMoyen += (listX[i] - moyenneListX) * (listX[i] - moyenneListX);
            }

            return ecartType * valeurStudent * Math.Sqrt(1 + (1 / nbDonnee) +
                   (((donneeEstime - moyenneListX) * (donneeEstime - moyenneListX)) / sommeXMoyen));
        }
    }
}