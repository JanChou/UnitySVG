//using System.Collections;

//namespace Unity.SVG.DOM {
	public class uSVGPointList : uSVGList {
		private ulong m_numberOfItems;
		/***********************************************************************************/
		private void f_Clear() {
		}
   		public uSVGPoint Initialize(uSVGPoint newItem) {
			return (uSVGPoint) base.Initialize(newItem);
		}

		public new uSVGPoint GetItem(uint index) {
			return (uSVGPoint) base.GetItem(index);
		}

    	public uSVGPoint InsertItemBefore(uSVGPoint newItem, uint index) {
			return (uSVGPoint) base.InsertItemBefore(newItem, index);
		}

    	public uSVGPoint ReplaceItem(uSVGPoint newItem, uint index) {
			return (uSVGPoint) base.ReplaceItem(newItem, index);
		}

		public new uSVGPoint RemoveItem(uint index) {
			return (uSVGPoint) base.RemoveItem(index);
		}

		public uSVGPoint AppendItem(uSVGPoint newItem) {
			return (uSVGPoint) base.AppendItem(newItem);
		}


	public void FromString(string listString) {
      // remove existing list items
      Clear();

      if ( listString != null )
      {
        int len = listString.Length; // temp

        if ( len > 0 )
        {
          int p = 0; // pos
          char c; // temp
          int sNum = -1; // start of the number
          int eNum = -1; // end of the number
          bool seenComma = false; // to handle 123,,123
          int tempSNum = -1; // start of the number (held in temp until two numbers are found)
          int tempENum = -1; // end of the number (held in temp until two numbers are found)

          // This used to be a regex-- it is *much* faster this way
          while (p<len)
          {
            // Get the char in a temp
            c = listString[p];

            // TODO: worry about NEL?
            if ((c=='\t') || (c=='\r') || (c=='\n') || (c==0x20) || (c==','))
            {
              // Special handling for two commas
              if (c==',') 
              {
                if (seenComma && sNum < 0)
                  throw new uSVGException(uSVGExceptionType.SvgInvalidValueErr);
                  
                seenComma = true;
              }

              // Are we in a number?
              if (sNum >= 0) 
              {
                // The end of the number is the previous char
                eNum = p-1;

                // Is this the x or y?
                if (tempSNum == -1) 
                {
                  // must be the x, hang onto it for a second
                  tempSNum = sNum;
                  tempENum = eNum;
                }
                else 
                {
                  // must be the y, use temp as x and append the item
                  AppendItem( new uSVGPoint(uSVGNumber.ParseToFloat(listString.Substring(tempSNum, (tempENum-tempSNum)+1)), 
                    uSVGNumber.ParseToFloat(listString.Substring(sNum, (eNum-sNum)+1))) );
                  tempSNum = -1;
                  tempENum = -1;
                }

                // Reset
                sNum = -1;
                eNum = -1;
                seenComma = false;
              }
            } 
            else if (sNum == -1)
              sNum = p;
            // OPTIMIZE: Right here we could check for [Ee] to save some time in IndexOfAny later
                  
            // Move to next char
            p++;
          }

          // We need to handle the end of the buffer as a delimiter
          if (sNum >= 0) 
          {
            if (tempSNum == -1)
              throw new uSVGException(uSVGExceptionType.SvgInvalidValueErr);

            // The end of the number is the previous char
            eNum = p-1;
            // must be the y, use temp as x and append the item
            AppendItem( new uSVGPoint(uSVGNumber.ParseToFloat(listString.Substring(tempSNum, (tempENum-tempSNum)+1)), 
              uSVGNumber.ParseToFloat(listString.Substring(sNum, (eNum-sNum)+1))) );
          }
          else if (tempSNum != -1) 
          {
            throw new uSVGException(uSVGExceptionType.SvgInvalidValueErr);
          }
        }                  
      }
    }
  }
//}
