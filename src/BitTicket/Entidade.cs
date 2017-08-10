using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitMarquee
{
    public class Exchanges
    {
        public int NEG { get; set; }
        public int MBT { get; set; }
        public int LOC { get; set; }
        public int FOX { get; set; }
        public int FLW { get; set; }
        public int B2U { get; set; }
    }

    public class Timestamp
    {
        public int total { get; set; }
        public Exchanges exchanges { get; set; }
    }

    public class Total : Price
    { }

    public class B2U : Price
    {
       
    }

    public class FLW : Price
    {
       
    }

    public class FOX : Price
    {
       
    }

    public class LOC : Price
    {
        
    }

    public class MBT : Price
    {
        
    }

    public class NEG : Price
    {
        
    }

    public class Exchanges2
    {
        public B2U B2U { get; set; }
        public FLW FLW { get; set; }
        public FOX FOX { get; set; }
        public LOC LOC { get; set; }
        public MBT MBT { get; set; }
        public NEG NEG { get; set; }
    }

    public class Ticker24h
    {
        public Total total { get; set; }
        public Exchanges2 exchanges { get; set; }
    }

    public class Total2 : Price
    {
        
    }

    public class B2U2 : Price
    {
        
    }

    public class FLW2 : Price
    {
        
    }

    public class FOX2 : Price
    {
        
    }

    public class LOC2 : Price
    {
        
    }

    public class MBT2 : Price
    {
    }

    public class NEG2 : Price
    {
        
    }

    public class Exchanges3
    {
        public B2U2 B2U { get; set; }
        public FLW2 FLW { get; set; }
        public FOX2 FOX { get; set; }
        public LOC2 LOC { get; set; }
        public MBT2 MBT { get; set; }
        public NEG2 NEG { get; set; }
    }

    public class Ticker12h
    {
        public Total2 total { get; set; }
        public Exchanges3 exchanges { get; set; }
    }

    public class Total3 : Price
    {
    }

    public class B2U3 : Price
    {
    }

    public class FOX3 : Price
    {
    }

    public class Price
    {
        public double last { get; set; }
        public double open { get; set; }
        public double high { get; set; }
        public double low { get; set; }
        public double vol { get; set; }
        public double vwap { get; set; }
        public double money { get; set; }
        public int trades { get; set; }
    }

    public class LOC3 : Price
    {
        
    }

    public class MBT3 : Price
    {
        
    }

    public class Exchanges4
    {
        public B2U3 B2U { get; set; }
        public FOX3 FOX { get; set; }
        public LOC3 LOC { get; set; }
        public MBT3 MBT { get; set; }
    }

    public class Ticker1h
    {
        public Total3 total { get; set; }
        public Exchanges4 exchanges { get; set; }
    }

    public class Rates
    {
        public double USDCBRL { get; set; }
        public double USDTBRL { get; set; }
    }

    public class RootObject
    {
        public Timestamp timestamp { get; set; }
        public Ticker24h ticker_24h { get; set; }
        public Ticker12h ticker_12h { get; set; }
        public Ticker1h ticker_1h { get; set; }
        public Rates rates { get; set; }
    }
}
