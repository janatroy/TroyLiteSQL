
function nbToggleBlock(elem, closed, open) 
{
	if(elem.getAttribute("expanded") == null) {
		return;
	}
	else
	{
		var isExpanded = (elem.getAttribute("expanded") == "true");
		
		if(isExpanded == true) {
			return;
		}
		else
		{
			var block = elem.parentElement;
			var navbar = block.parentElement;
			
			var allblocks = navbar.getElementsByTagName("div");
			var expanded;
			
			for(var i=0; i<allblocks.length; i++) {
				if(allblocks[i].getAttribute("expanded") != null && allblocks[i].getAttribute("expanded") == "true") 
				{
					expanded = allblocks[i];
					break;
				}
			}
			
			if(expanded != null) {
				expanded.setAttribute("expanded", "false");
				expanded.className = closed;
				var childdiv = expanded.parentElement.childNodes[1];
				if(childdiv != null) {
					childdiv.style.display = "none";
				}
			}
			
			expanded = block.childNodes[0];
			if(expanded != null) {
				expanded.className = open;
				expanded.setAttribute("expanded", "true");
				var now = new Date();
				fixDate(now);
				now.setTime(now.getTime() + 30 * 60 * 1000);
				nbsetCookie("Expanded", block.id, now);
			}
			
			var openingdiv = block.childNodes[1];
			if(openingdiv != null) {
				openingdiv.style.display = "block";
			}
		}
	}
}

function nbItemHighlight(elem, style) 
{
	elem.className = style;
	elem.parentElement.className = style;
}

function nbItemLowlight(elem, style) 
{
	elem.className = style;
	elem.parentElement.className = style;
}

function setSelectedCookie(elem, style) {
	var now = new Date();
	fixDate(now);
	now.setTime(now.getTime() + 30 * 60 * 1000);
	nbsetCookie("Selected", elem, now);
	var ctl = document.getElementById(elem);
	if(ctl!=null) {
		ctl.className = style;
	}
}
function nbReplace(elem, url, style) 
{
	setSelectedCookie(elem, style);
	window.location.replace(unescape(url));
}

function nbNavigate(elem, url, style) 
{
	setSelectedCookie(elem, style);
	window.location.href = unescape(url);
}

function nbsetCookie(name, value, expires, path, domain, secure) {
  var curCookie = name + "=" + escape(value) +
      ((expires) ? "; expires=" + expires.toGMTString() : "") +
      "; path=/;";
  document.cookie = curCookie;
}


function nbgetCookie(name) {
  var dc = document.cookie;
  var prefix = name + "=";
  var begin = dc.indexOf("; " + prefix);
  if (begin == -1) {
    begin = dc.indexOf(prefix);
    if (begin != 0) return null;
  } else
    begin += 2;
  var end = document.cookie.indexOf(";", begin);
  if (end == -1)
    end = dc.length;
  return unescape(dc.substring(begin + prefix.length, end));
}

function nbdeleteCookie(name, domain) {
  if (nbgetCookie(name)) {
    document.cookie = name + "=" +
    ((domain) ? "; domain=" + domain : "") +
    "; expires=Thu, 01-Jan-70 00:00:01 GMT";
  }
}

function fixDate(date) {
  var base = new Date(0);
  var skew = base.getTime();
  if (skew > 0)
    date.setTime(date.getTime() - skew);
}

function clickButton(e, buttonid)
{ 
  var evt = e ? e : window.event;
  var bt = document.getElementById(buttonid);

  if (bt)
  { 
      if (evt.keyCode == 13)
      { 
         bt.click(); 
         return false; 
      } 
  } 
  
}

function getTags(tagName) 
{ 
        if (!document.getElementsByTagName) return null; 
                return document.getElementsByTagName(tagName); 
} 

function SetSize() 
{ 
        var divs = getTags('div'); 
        
        for (var i = 0; i < divs.length; i++) 
        { 
                if (divs[i].className.toLowerCase() == 'divbody') 
                { 
                        divs[i].style.height = document.documentElement.clientWidth / 2.25; 
                } 
        } 
}       

function GettxtBoxName(txt)
{
	var tags = getTags("input");
	if (tags == null) return null;
	
	for (i = 0; i < tags.length; i++)
	{	
		if(tags[i].name.indexOf(txt) != -1)
		    return tags[i].name;
	}
	
}

function clock()
{
          var time = new Date()
          var hr = time.getHours()
          var min = time.getMinutes()
          var sec = time.getSeconds()
          
                                
           var temp = "" + ((hr > 12) ? hr - 12 : hr)
           if(hr==0) temp = "12"
           if(temp.length==1) temp = " " + temp
           temp += ((min < 10) ? ":0" : ":") + min
           temp += ((sec < 10) ? ":0" : ":") + sec
           temp += (hr >= 12) ? " PM" : " AM"
          if(hr < 10){
            hr = " " + hr
            }
          if(min < 10){
            min = "0" + min
            }
          if(sec < 10){
            sec = "0" + sec
            } 
            
          document.getElementById('ctl00_lblTitle').innerHTML = temp
          setTimeout("clock()", 1000)
          
}

