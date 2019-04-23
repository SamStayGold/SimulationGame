using System;
using System.Collections;
using System.Collections.Generic;


// the collection of basic propertys
public class BasicPropertys
{    public int frontend;
     public int backend;
     public int creativity;

     public BasicPropertys(int frontend, int backend, int creativity)
     {   this.frontend = frontend;
         this.backend = backend;
         this.creativity = creativity;
     }

// give a copy of the current propertys
     public BasicPropertys givecopy()
     {   return new BasicPropertys(this.frontend,this.backend
         ,this.creativity);
     }

// add from input to the current propertys
// current = input
     public void add_propertys(BasicPropertys input)
     {   this.frontend += input.frontend;
         this.backend += input.backend;
         this.creativity += input.creativity;
     }

 
}
