﻿using System;
namespace CS.Data.Entities
{

    public enum TipActIdentitate
    {
        BI = 0,
        CI = 1,
        Pasaport = 2,
        Deces = 3
    }

    public enum TipLocalitate
    {
        Sat = 0,
        Comuna = 1,
        Oras = 2,
        Municipiu = 3
    }

    public enum Sex
    {
        M=1,
        F=2
    }

    public enum CatFol
    {
        A = 0,
        V = 1,
        N = 2,
        CC = 3
    }

    public enum TipPersoana
    {
        //fizica
        F=0,
        //juridica
        J=1,
        //neidentificat
        N=2
    }

    enum TipPrefixPJ
    {
        SC=0,
        SNC=1
    }

    enum TipSufixPJ
    {
        SRL=0,
        SA=1
    }
}
