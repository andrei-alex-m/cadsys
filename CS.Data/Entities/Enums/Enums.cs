using System;
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
        M = 1,
        F = 2
    }

    public enum CatFol
    {
        /*A = 0,
        V = 1,
        N = 2,
        CC = 3,
        P = 4,
        DR = 5,
        HB = 6,
        PD = 7,
        ALTELE = 8*/

        A = 1,
        ALTELE = 2,
        CC = 3,
        CF = 4,
        DR = 5,
        F = 6,
        HB = 7,
        HR = 8,
        L = 9,
        N = 10,
        NA = 11,
        P = 12,
        PD = 13,
        V = 14


    }

    public enum TipPersoana
    {
        //fizica
        F = 0,
        //juridica
        J = 1,
        //neidentificat
        N = 2
    }

    enum TipPrefixPJ
    {
        SC = 0,
        SNC = 1
    }

    enum TipSufixPJ
    {
        SRL = 0,
        SA = 1
    }
}
