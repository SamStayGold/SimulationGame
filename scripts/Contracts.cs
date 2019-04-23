public class Contract
{   private BasicPropertys propertys;
    private string name;
    private int award;
    private int level;

    public Contract(string name, int award, int level, BasicPropertys propertys)
    {   this.name = name;
        this.award = award;
        this.level = level;
        this.propertys = propertys;
    }

    public string get_name() { return name;}
    public int get_level() { return level;}
    public int get_award() { return award;}
    public BasicPropertys get_propertys()
    {   return propertys;
    }

    public string get_detail()
    {   return "Level "+level+"--"+name+" worths "+award+"$;";
    }

}
