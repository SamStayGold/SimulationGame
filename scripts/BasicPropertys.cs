using System;
using System.Collections;
using System.Collections.Generic;

// the collection of basic propertys

[Serializable]
public class BasicPropertys
{    private Dictionary<Property,int> propertys = new Dictionary<Property,int>();
     private int _frontend;
     public int Frontend { get{ return propertys[Property.Frontend];}
     set{propertys[Property.Frontend] = value;}}

     private int _backend;
     public int Backend { get{ return propertys[Property.Backend];}
     set{propertys[Property.Backend] = value;}}

     private int _graphics;
     public int Graphics { get{ return propertys[Property.Graphics];}
     set{propertys[Property.Graphics] = value;}}

     public BasicPropertys(int frontend, int backend, int graphics)
     {   this.propertys.Add(Property.Frontend,frontend);
         this.propertys.Add(Property.Backend,backend);
         this.propertys.Add(Property.Graphics,graphics);
     }

     public void setPropertyQuant(Property target, int quant)
     {   propertys[target] = quant;
     }
     public void updatePropertyQuant(Property target, int quant)
     {   propertys[target] += quant;
     }
     public int getPropertyQuant(Property target)
     {   return propertys[target];
     }


// give a copy of the current propertys
     public BasicPropertys givecopy()
     {   return new BasicPropertys(propertys[Property.Frontend]
         ,propertys[Property.Backend]
         ,propertys[Property.Graphics]);
     }

     public List<Property> getListOfProperty()
     {   List<Property> listOfProperty = new List<Property>();
         foreach(Property p in propertys.Keys)
         {   listOfProperty.Add(p);
         }
         return listOfProperty;
     }

// add from input to the current propertys
// current = input

     public void add_propertys(BasicPropertys input)
     {   this.Frontend += input.Frontend;
         this.Backend += input.Backend;
         this.Graphics += input.Graphics;
     }

     public Property getMaxBasicProperty(List<Property> amongThesePropertys)
     {   int max = 0;
        // currently property is not nullable
        // so this is nasty
         Property maxProperty = Property.Frontend;

         foreach(KeyValuePair<Property,int> entry in propertys)
         {   if(amongThesePropertys.Contains(entry.Key))
             {   if(entry.Value>max)
                 {   max = entry.Value;
                     maxProperty = entry.Key;
                 }
             }
         }

         return maxProperty;
     }

}
