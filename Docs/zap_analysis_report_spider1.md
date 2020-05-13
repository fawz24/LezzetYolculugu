
# ZAP Scanning Report




## Summary of Alerts

| Risk Level | Number of Alerts |
| --- | --- |
| High | 0 |
| Medium | 1 |
| Low | 4 |
| Informational | 3 |

## Alert Detail


  
  
  
### X-Frame-Options Header Not Set
##### Medium (Medium)
  
  
  
  
#### Description
<p>X-Frame-Options header is not included in the HTTP response to protect against 'ClickJacking' attacks.</p>
  
  
  
* URL: [http://localhost:55131/](http://localhost:55131/)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
  
* URL: [http://localhost:55131/Recipe/Details/7](http://localhost:55131/Recipe/Details/7)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
  
* URL: [http://localhost:55131/Account/SignUp](http://localhost:55131/Account/SignUp)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
  
* URL: [http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F4](http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F4)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
  
* URL: [http://localhost:55131/Recipe/Details/6](http://localhost:55131/Recipe/Details/6)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
  
* URL: [http://localhost:55131/Recipe/Index?id=1](http://localhost:55131/Recipe/Index?id=1)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
  
* URL: [http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F5](http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F5)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
  
* URL: [http://localhost:55131/Recipe/Details/5](http://localhost:55131/Recipe/Details/5)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
  
* URL: [http://localhost:55131/Recipe/Index?id=2](http://localhost:55131/Recipe/Index?id=2)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
  
* URL: [http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F6](http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F6)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
  
* URL: [http://localhost:55131/Recipe/Details/4](http://localhost:55131/Recipe/Details/4)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
  
* URL: [http://localhost:55131/index/](http://localhost:55131/index/)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
  
* URL: [http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F7](http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F7)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
  
* URL: [http://localhost:55131/Recipe/](http://localhost:55131/Recipe/)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
  
* URL: [http://localhost:55131/Recipe/Details/3](http://localhost:55131/Recipe/Details/3)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
  
* URL: [http://localhost:55131/Recipe/Index?id=0](http://localhost:55131/Recipe/Index?id=0)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
  
* URL: [http://localhost:55131/Account/SignUp](http://localhost:55131/Account/SignUp)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
  
* URL: [http://localhost:55131/Recipe](http://localhost:55131/Recipe)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
  
* URL: [http://localhost:55131/Account/SignIn?ReturnUrl=%2Fadmin%2F](http://localhost:55131/Account/SignIn?ReturnUrl=%2Fadmin%2F)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
  
* URL: [http://localhost:55131/Recipe/Index](http://localhost:55131/Recipe/Index)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
  
Instances: 20
  
### Solution
<p>Most modern Web browsers support the X-Frame-Options HTTP header. Ensure it's set on all web pages returned by your site (if you expect the page to be framed only by pages on your server (e.g. it's part of a FRAMESET) then you'll want to use SAMEORIGIN, otherwise if you never expect the page to be framed, you should use DENY. ALLOW-FROM allows specific websites to frame the web page in supported web browsers).</p>
  
### Reference
* https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-Frame-Options

  
#### CWE Id : 16
  
#### WASC Id : 15
  
#### Source ID : 3

  
  
  
### Server Leaks Information via "X-Powered-By" HTTP Response Header Field(s)
##### Low (Medium)
  
  
  
  
#### Description
<p>The web/application server is leaking information via one or more "X-Powered-By" HTTP response headers. Access to such information may facilitate attackers identifying other frameworks/components your web application is reliant upon and the vulnerabilities such components may be subject to.</p>
  
  
  
* URL: [http://localhost:55131/lib/bootstrap/dist/js/bootstrap.bundle.js](http://localhost:55131/lib/bootstrap/dist/js/bootstrap.bundle.js)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [http://localhost:55131/css/site.css](http://localhost:55131/css/site.css)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F5](http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F5)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [http://localhost:55131/lib/bootstrap/dist](http://localhost:55131/lib/bootstrap/dist)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [http://localhost:55131/api/recipes/3](http://localhost:55131/api/recipes/3)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [http://localhost:55131/robots.txt](http://localhost:55131/robots.txt)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [http://localhost:55131/js/site.js](http://localhost:55131/js/site.js)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [http://localhost:55131/account/](http://localhost:55131/account/)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F6](http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F6)
  
  
  * Method: `POST`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [http://localhost:55131/api/recipes/3/comments](http://localhost:55131/api/recipes/3/comments)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [http://localhost:55131/Account/SignIn?ReturnUrl=%2F](http://localhost:55131/Account/SignIn?ReturnUrl=%2F)
  
  
  * Method: `POST`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [http://localhost:55131/Account/SignIn?ReturnUrl=%2Fadmin%2F](http://localhost:55131/Account/SignIn?ReturnUrl=%2Fadmin%2F)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [http://localhost:55131/lib/jquery/dist/jquery.js](http://localhost:55131/lib/jquery/dist/jquery.js)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F6](http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F6)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [http://localhost:55131/lib/bootstrap/dist/js](http://localhost:55131/lib/bootstrap/dist/js)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [http://localhost:55131/Recipe/Index](http://localhost:55131/Recipe/Index)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F7](http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F7)
  
  
  * Method: `POST`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [http://localhost:55131/lib](http://localhost:55131/lib)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [http://localhost:55131/api](http://localhost:55131/api)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [http://localhost:55131/Account/SignIn?ReturnUrl=%2Fadmin%2F](http://localhost:55131/Account/SignIn?ReturnUrl=%2Fadmin%2F)
  
  
  * Method: `POST`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
Instances: 69
  
### Solution
<p>Ensure that your web server, application server, load balancer, etc. is configured to suppress "X-Powered-By" headers.</p>
  
### Reference
* http://blogs.msdn.com/b/varunm/archive/2013/04/23/remove-unwanted-http-response-headers.aspx
* http://www.troyhunt.com/2012/02/shhh-dont-let-your-response-headers.html

  
#### CWE Id : 200
  
#### WASC Id : 13
  
#### Source ID : 3

  
  
  
### X-Content-Type-Options Header Missing
##### Low (Medium)
  
  
  
  
#### Description
<p>The Anti-MIME-Sniffing header X-Content-Type-Options was not set to 'nosniff'. This allows older versions of Internet Explorer and Chrome to perform MIME-sniffing on the response body, potentially causing the response body to be interpreted and displayed as a content type other than the declared content type. Current (early 2014) and legacy versions of Firefox will use the declared content type (if one is set), rather than performing MIME-sniffing.</p>
  
  
  
* URL: [http://localhost:55131/Recipe/Details/7](http://localhost:55131/Recipe/Details/7)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [http://localhost:55131/Account/SignUp](http://localhost:55131/Account/SignUp)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [http://localhost:55131/api/recipes/6/comments](http://localhost:55131/api/recipes/6/comments)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [http://localhost:55131/lib/bootstrap/dist/css/bootstrap.css](http://localhost:55131/lib/bootstrap/dist/css/bootstrap.css)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [http://localhost:55131/Recipe/Index?id=2](http://localhost:55131/Recipe/Index?id=2)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [http://localhost:55131/Recipe](http://localhost:55131/Recipe)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [http://localhost:55131/Recipe/Details/6](http://localhost:55131/Recipe/Details/6)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [http://localhost:55131/api/recipes/7/comments](http://localhost:55131/api/recipes/7/comments)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F3](http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F3)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [http://localhost:55131/Account/SignUp](http://localhost:55131/Account/SignUp)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [http://localhost:55131/api/recipes/4/comments](http://localhost:55131/api/recipes/4/comments)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [http://localhost:55131/js/site.js](http://localhost:55131/js/site.js)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [http://localhost:55131/Account/SignIn](http://localhost:55131/Account/SignIn)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [http://localhost:55131/lib/bootstrap/dist/js/bootstrap.bundle.js](http://localhost:55131/lib/bootstrap/dist/js/bootstrap.bundle.js)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [http://localhost:55131/lib/jquery/dist/jquery.js](http://localhost:55131/lib/jquery/dist/jquery.js)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [http://localhost:55131/Recipe/Index](http://localhost:55131/Recipe/Index)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [http://localhost:55131/Recipe/Details/3](http://localhost:55131/Recipe/Details/3)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F6](http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F6)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [http://localhost:55131/Recipe/](http://localhost:55131/Recipe/)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [http://localhost:55131/js/site.js?v=4q1jwFhaPaZgr8WAUSrux6hAuh0XDg9kPS3xIVq36I0](http://localhost:55131/js/site.js?v=4q1jwFhaPaZgr8WAUSrux6hAuh0XDg9kPS3xIVq36I0)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
Instances: 33
  
### Solution
<p>Ensure that the application/web server sets the Content-Type header appropriately, and that it sets the X-Content-Type-Options header to 'nosniff' for all web pages.</p><p>If possible, ensure that the end user uses a standards-compliant and modern web browser that does not perform MIME-sniffing at all, or that can be directed by the web application/web server to not perform MIME-sniffing.</p>
  
### Other information
<p>This issue still applies to error type pages (401, 403, 500, etc.) as those pages are often still affected by injection issues, in which case there is still concern for browsers sniffing pages away from their actual content type.</p><p>At "High" threshold this scanner will not alert on client or server error responses.</p>
  
### Reference
* http://msdn.microsoft.com/en-us/library/ie/gg622941%28v=vs.85%29.aspx
* https://www.owasp.org/index.php/List_of_useful_HTTP_headers

  
#### CWE Id : 16
  
#### WASC Id : 15
  
#### Source ID : 3

  
  
  
### Application Error Disclosure
##### Low (Medium)
  
  
  
  
#### Description
<p>This page contains an error/warning message that may disclose sensitive information like the location of the file that produced the unhandled exception. This information can be used to launch further attacks against the web application. The alert could be a false positive if the error message is found inside a documentation page.</p>
  
  
  
* URL: [http://localhost:55131/Account](http://localhost:55131/Account)
  
  
  * Method: `GET`
  
  
  * Evidence: `HTTP/1.1 500 Internal Server Error`
  
  
  
  
* URL: [http://localhost:55131/account/](http://localhost:55131/account/)
  
  
  * Method: `GET`
  
  
  * Evidence: `HTTP/1.1 500 Internal Server Error`
  
  
  
  
* URL: [http://localhost:55131/Account/](http://localhost:55131/Account/)
  
  
  * Method: `GET`
  
  
  * Evidence: `HTTP/1.1 500 Internal Server Error`
  
  
  
  
Instances: 3
  
### Solution
<p>Review the source code of this page. Implement custom error pages. Consider implementing a mechanism to provide a unique error reference/identifier to the client (browser) while logging the details on the server side and not exposing them to the user.</p>
  
### Reference
* 

  
#### CWE Id : 200
  
#### WASC Id : 13
  
#### Source ID : 3

  
  
  
### Information Disclosure - Debug Error Messages
##### Low (Medium)
  
  
  
  
#### Description
<p>The response appeared to contain common error messages returned by platforms such as ASP.NET, and Web-servers such as IIS and Apache. You can configure the list of common debug messages.</p>
  
  
  
* URL: [http://localhost:55131/Account](http://localhost:55131/Account)
  
  
  * Method: `GET`
  
  
  * Evidence: `Internal Server Error`
  
  
  
  
* URL: [http://localhost:55131/account/](http://localhost:55131/account/)
  
  
  * Method: `GET`
  
  
  * Evidence: `Internal Server Error`
  
  
  
  
* URL: [http://localhost:55131/Account/](http://localhost:55131/Account/)
  
  
  * Method: `GET`
  
  
  * Evidence: `Internal Server Error`
  
  
  
  
Instances: 3
  
### Solution
<p>Disable debugging messages before pushing to production.</p>
  
### Reference
* 

  
#### CWE Id : 200
  
#### WASC Id : 13
  
#### Source ID : 3

  
  
  
### Loosely Scoped Cookie
##### Informational (Low)
  
  
  
  
#### Description
<p>Cookies can be scoped by domain or path. This check is only concerned with domain scope.The domain scope applied to a cookie determines which domains can access it. For example, a cookie can be scoped strictly to a subdomain e.g. www.nottrusted.com, or loosely scoped to a parent domain e.g. nottrusted.com. In the latter case, any subdomain of nottrusted.com can access the cookie. Loosely scoped cookies are common in mega-applications like google.com and live.com. Cookies set from a subdomain like app.foo.bar are transmitted only to that domain by the browser. However, cookies scoped to a parent-level domain may be transmitted to the parent, or any subdomain of the parent.</p>
  
  
  
* URL: [http://localhost:55131/Account/SignOut](http://localhost:55131/Account/SignOut)
  
  
  * Method: `GET`
  
  
  
  
* URL: [http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F3](http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F3)
  
  
  * Method: `GET`
  
  
  
  
* URL: [http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F6](http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F6)
  
  
  * Method: `POST`
  
  
  
  
* URL: [http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F3](http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F3)
  
  
  * Method: `POST`
  
  
  
  
* URL: [http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F4](http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F4)
  
  
  * Method: `POST`
  
  
  
  
* URL: [http://localhost:55131/Account/SignIn?ReturnUrl=%2F](http://localhost:55131/Account/SignIn?ReturnUrl=%2F)
  
  
  * Method: `POST`
  
  
  
  
* URL: [http://localhost:55131/Account/SignIn?ReturnUrl=%2Fadmin%2F](http://localhost:55131/Account/SignIn?ReturnUrl=%2Fadmin%2F)
  
  
  * Method: `POST`
  
  
  
  
* URL: [http://localhost:55131/Account/SignIn](http://localhost:55131/Account/SignIn)
  
  
  * Method: `GET`
  
  
  
  
* URL: [http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F7](http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F7)
  
  
  * Method: `POST`
  
  
  
  
* URL: [http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F5](http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F5)
  
  
  * Method: `POST`
  
  
  
  
Instances: 10
  
### Solution
<p>Always scope cookies to a FQDN (Fully Qualified Domain Name).</p>
  
### Other information
<p>The origin domain used for comparison was: </p><p>localhost</p><p>AuthenticationId=</p><p>Identity.External=</p><p>Identity.TwoFactorUserId=</p><p></p>
  
### Reference
* https://tools.ietf.org/html/rfc6265#section-4.1
* https://www.owasp.org/index.php/Testing_for_cookies_attributes_(OTG-SESS-002)
* http://code.google.com/p/browsersec/wiki/Part2#Same-origin_policy_for_cookies

  
#### CWE Id : 565
  
#### WASC Id : 15
  
#### Source ID : 3

  
  
  
### Timestamp Disclosure - Unix
##### Informational (Low)
  
  
  
  
#### Description
<p>A timestamp was disclosed by the application/web server - Unix</p>
  
  
  
* URL: [http://localhost:55131/lib/jquery/dist/jquery.js](http://localhost:55131/lib/jquery/dist/jquery.js)
  
  
  * Method: `GET`
  
  
  * Evidence: `20110929`
  
  
  
  
* URL: [http://localhost:55131/lib/jquery/dist/jquery.js](http://localhost:55131/lib/jquery/dist/jquery.js)
  
  
  * Method: `GET`
  
  
  * Evidence: `20030331`
  
  
  
  
Instances: 2
  
### Solution
<p>Manually confirm that the timestamp data is not sensitive, and that the data cannot be aggregated to disclose exploitable patterns.</p>
  
### Other information
<p>20110929, which evaluates to: 1970-08-21 21:22:09</p>
  
### Reference
* https://www.owasp.org/index.php/Top_10_2013-A6-Sensitive_Data_Exposure
* http://projects.webappsec.org/w/page/13246936/Information%20Leakage

  
#### CWE Id : 200
  
#### WASC Id : 13
  
#### Source ID : 3

  
  
  
### Information Disclosure - Suspicious Comments
##### Informational (Low)
  
  
  
  
#### Description
<p>The response appears to contain suspicious comments which may help an attacker. Note: Matches made within script blocks or files are against the entire content not only comments.</p>
  
  
  
* URL: [http://localhost:55131/lib/jquery/dist/jquery.js](http://localhost:55131/lib/jquery/dist/jquery.js)
  
  
  * Method: `GET`
  
  
  
  
* URL: [http://localhost:55131/lib/bootstrap/dist/js/bootstrap.bundle.js](http://localhost:55131/lib/bootstrap/dist/js/bootstrap.bundle.js)
  
  
  * Method: `GET`
  
  
  
  
Instances: 2
  
### Solution
<p>Remove all comments that return information that may help an attacker and fix any underlying problems they refer to.</p>
  
### Other information
<p>The following comment/snippet was identified via the pattern: \bWHERE\b</p><p>		// For CommonJS and CommonJS-like environments where a proper `window`</p><p>The following comment/snippet was identified via the pattern: \bFROM\b</p><p>		// Return just the one element from the set</p><p>The following comment/snippet was identified via the pattern: \bSELECT\b</p><p>	select,</p><p>The following comment/snippet was identified via the pattern: \bSELECT\b</p><p>		// We use this for POS matching in `select`</p><p>The following comment/snippet was identified via the pattern: \bSELECT\b</p><p>	rinputs = /^(?:input|select|textarea|button)$/i,</p><p>The following comment/snippet was identified via the pattern: \bFROM\b</p><p>	// Return early from calls with invalid selector or context</p><p>The following comment/snippet was identified via the pattern: \bWHERE\b</p><p>			// (excepting DocumentFragment context, where the methods don't exist)</p><p>The following comment/snippet was identified via the pattern: \bTODO\b</p><p>							// TODO: identify versions</p><p>The following comment/snippet was identified via the pattern: \bTODO\b</p><p>						// TODO: identify versions</p><p>The following comment/snippet was identified via the pattern: \bSELECT\b</p><p>	return select( selector.replace( rtrim, "$1" ), context, results, seed );</p><p>The following comment/snippet was identified via the pattern: \bFROM\b</p><p>		// Remove from its parent by default</p><p>The following comment/snippet was identified via the pattern: \bWHERE\b</p><p>					// Where there is no isDisabled, check manually</p><p>The following comment/snippet was identified via the pattern: \bWHERE\b</p><p>	// documentElement is verified for cases where it doesn't yet exist</p><p>The following comment/snippet was identified via the pattern: \bBUG\b</p><p>	// We allow this because of a bug in IE8/9 that throws an error</p><p>The following comment/snippet was identified via the pattern: \bBUGS\b</p><p>	// See https://bugs.jquery.com/ticket/13378</p><p>The following comment/snippet was identified via the pattern: \bFROM\b</p><p>		// Regex strategy adopted from Diego Perini</p><p>The following comment/snippet was identified via the pattern: \bSELECT\b</p><p>			// Select is set to empty string on purpose</p><p>The following comment/snippet was identified via the pattern: \bBUGS\b</p><p>			// https://bugs.jquery.com/ticket/12359</p><p>The following comment/snippet was identified via the pattern: \bSELECT\b</p><p>				"<select id='" + expando + "-\r\\' msallowcapture=''>" +</p><p>The following comment/snippet was identified via the pattern: \bSELECT\b</p><p>				"<option selected=''></option></select>";</p><p>The following comment/snippet was identified via the pattern: \bLATER\b</p><p>			// IE8 throws error here and will not see later tests</p><p>The following comment/snippet was identified via the pattern: \bBUGS\b</p><p>			// https://bugs.webkit.org/show_bug.cgi?id=136851</p><p>The following comment/snippet was identified via the pattern: \bSELECT\b</p><p>				"<select disabled='disabled'><option/></select>";</p><p>The following comment/snippet was identified via the pattern: \bLATER\b</p><p>			// IE8 throws error here and will not see later tests</p><p>The following comment/snippet was identified via the pattern: \bUSER\b</p><p>	// Can be adjusted by the user</p><p>The following comment/snippet was identified via the pattern: \bFROM\b</p><p>			/* matches from matchExpr["CHILD"]</p><p>The following comment/snippet was identified via the pattern: \bFROM\b</p><p>			// Strip excess characters from unquoted arguments</p><p>The following comment/snippet was identified via the pattern: \bFROM\b</p><p>				// Get excess from tokenize (recursively)</p><p>The following comment/snippet was identified via the pattern: \bFROM\b</p><p>							// Seek `elem` from a previously-cached index</p><p>The following comment/snippet was identified via the pattern: \bFROM\b</p><p>								// Fallback to seeking `elem` from the start</p><p>The following comment/snippet was identified via the pattern: \bFROM\b</p><p>								// Use the same loop as above to seek `elem` from the start</p><p>The following comment/snippet was identified via the pattern: \bFROM\b</p><p>			// Remember that setFilters inherits from pseudos</p><p>The following comment/snippet was identified via the pattern: \bUSER\b</p><p>			// The user may use createPseudo to indicate that</p><p>The following comment/snippet was identified via the pattern: \bFROM\b</p><p>			// We can't set arbitrary data on XML nodes, so they don't benefit from combinator caching</p><p>The following comment/snippet was identified via the pattern: \bFROM\b</p><p>			// Get initial elements from seed or context</p><p>The following comment/snippet was identified via the pattern: \bFROM\b</p><p>				// Move matched elements from seed to results to keep them synchronized</p><p>The following comment/snippet was identified via the pattern: \bFROM\b</p><p>		// The foundational matcher ensures that elements are reachable from top-level context(s)</p><p>The following comment/snippet was identified via the pattern: \bFROM\b</p><p>			// case, which will result in a "00" `matchedCount` that differs from `i` but is also</p><p>The following comment/snippet was identified via the pattern: \bSELECT\b</p><p>select = Sizzle.select = function( selector, context, results, seed ) {</p><p>The following comment/snippet was identified via the pattern: \bLATER\b</p><p>// Give the init function the jQuery prototype for later instantiation</p><p>The following comment/snippet was identified via the pattern: \bFROM\b</p><p>	// Methods guaranteed to produce a unique set when starting from a unique set</p><p>The following comment/snippet was identified via the pattern: \bFROM\b</p><p>	// Convert options from String-formatted to Object-formatted if needed</p><p>The following comment/snippet was identified via the pattern: \bFROM\b</p><p>					// If we have memory from a past run, we should fire after adding</p><p>The following comment/snippet was identified via the pattern: \bFROM\b</p><p>			// Remove a callback from the list</p><p>The following comment/snippet was identified via the pattern: \bFROM\b</p><p>			// Remove all callbacks from the list</p><p>The following comment/snippet was identified via the pattern: \bFROM\b</p><p>							// Re-resolve promises immediately to dodge false rejection from</p><p>The following comment/snippet was identified via the pattern: \bWHERE\b</p><p>// Catch cases where $(document).ready() is called</p><p>The following comment/snippet was identified via the pattern: \bWHERE\b</p><p>		// In cases where either:</p><p>The following comment/snippet was identified via the pattern: \bFROM\b</p><p>			// from DOM nodes, so set to undefined instead</p><p>The following comment/snippet was identified via the pattern: \bBUG\b</p><p>			// https://bugs.chromium.org/p/chromium/issues/detail?id=378607 (bug restricted)</p><p>The following comment/snippet was identified via the pattern: \bUSER\b</p><p>//	3. Use the same single mechanism to support "private" and "user" data.</p><p>The following comment/snippet was identified via the pattern: \bTODO\b</p><p>//	4. _Never_ expose "private" data to user code (TODO: Drop _data, _removeData)</p><p>The following comment/snippet was identified via the pattern: \bUSER\b</p><p>//	5. Avoid exposing implementation details on user objects (eg. expando properties)</p><p>The following comment/snippet was identified via the pattern: \bFROM\b</p><p>	// data from the HTML5 data-* attribute</p><p>The following comment/snippet was identified via the pattern: \bLATER\b</p><p>			// Make sure we set the data so it isn't changed later</p><p>The following comment/snippet was identified via the pattern: \bTODO\b</p><p>	// TODO: Now that all calls to _data and _removeData have been replaced</p><p>The following comment/snippet was identified via the pattern: \bFROM\b</p><p>				// Attempt to get data from the cache</p><p>The following comment/snippet was identified via the pattern: \bFROM\b</p><p>			// Add a progress sentinel to prevent the fx queue from being</p><p>The following comment/snippet was identified via the pattern: \bFROM\b</p><p>		// isHiddenWithinTree might be called from jQuery#filter function;</p><p>The following comment/snippet was identified via the pattern: \bFROM\b</p><p>		// Halve the iteration target value to prevent interference from CSS upper bounds (gh-2144)</p><p>The following comment/snippet was identified via the pattern: \bFROM\b</p><p>		// Iteratively approximate from a nonzero starting point</p><p>The following comment/snippet was identified via the pattern: \bLATER\b</p><p>		// Make sure we update the tween properties later on</p><p>The following comment/snippet was identified via the pattern: \bSELECT\b</p><p>	option: [ 1, "<select multiple='multiple'>", "</select>" ],</p><p>The following comment/snippet was identified via the pattern: \bFROM\b</p><p>	// Remove wrapper from fragment</p><p>The following comment/snippet was identified via the pattern: \bLATER\b</p><p>		// Make sure that the handler has a unique ID, used to find/remove it later</p><p>The following comment/snippet was identified via the pattern: \bFROM\b</p><p>	// Detach an event or set of events from an element</p><p>The following comment/snippet was identified via the pattern: \bFROM\b</p><p>		// Make a writable jQuery.Event from the native event object</p><p>The following comment/snippet was identified via the pattern: \bFROM\b</p><p>			// Prevent triggered image.load events from bubbling to window.load</p><p>The following comment/snippet was identified via the pattern: \bBUGS\b</p><p>// https://bugs.chromium.org/p/chromium/issues/detail?id=470258</p><p>The following comment/snippet was identified via the pattern: \bBUG\b</p><p>// for the description of the bug (it existed in older Chrome versions as well).</p><p>The following comment/snippet was identified via the pattern: \bUSER\b</p><p>	// 2. Copy user data</p><p>The following comment/snippet was identified via the pattern: \bBUGS\b</p><p>// Fix IE bugs, see support tests</p><p>The following comment/snippet was identified via the pattern: \bLATER\b</p><p>					// Keep references to cloned scripts for later restoration</p><p>The following comment/snippet was identified via the pattern: \bFROM\b</p><p>		// Copy the events from the original to the clone</p><p>The following comment/snippet was identified via the pattern: \bFROM\b</p><p>					// We should always get a number back from opacity</p><p>The following comment/snippet was identified via the pattern: \bQUERY\b</p><p>		// want to query the value if it is a CSS custom property</p><p>The following comment/snippet was identified via the pattern: \bUSER\b</p><p>		// since they are user-defined.</p><p>The following comment/snippet was identified via the pattern: \bBUG\b</p><p>				// Fixes bug #9237</p><p>The following comment/snippet was identified via the pattern: \bFROM\b</p><p>			// If a hook was provided get the non-computed value from there</p><p>The following comment/snippet was identified via the pattern: \bFROM\b</p><p>			// Otherwise just get the value from the style object</p><p>The following comment/snippet was identified via the pattern: \bUSER\b</p><p>		// since they are user-defined.</p><p>The following comment/snippet was identified via the pattern: \bFROM\b</p><p>		// If a hook was provided get the computed value from there</p><p>The following comment/snippet was identified via the pattern: \bWHERE\b</p><p>			// Use .style if available and use plain properties where available.</p><p>The following comment/snippet was identified via the pattern: \bFROM\b</p><p>				// there is still data from a stopped show/hide</p><p>The following comment/snippet was identified via the pattern: \bFROM\b</p><p>		// from identically-valued overflowX and overflowY and Edge just mirrors</p><p>The following comment/snippet was identified via the pattern: \bBUG\b</p><p>				// Archaic crash bug won't allow us to use `1 - ( 0.5 || 0 )` (#12497)</p><p>The following comment/snippet was identified via the pattern: \bFROM\b</p><p>	// Attach callbacks from options</p><p>The following comment/snippet was identified via the pattern: \bSELECT\b</p><p>		select = document.createElement( "select" ),</p><p>The following comment/snippet was identified via the pattern: \bSELECT\b</p><p>		opt = select.appendChild( document.createElement( "option" ) );</p><p>The following comment/snippet was identified via the pattern: \bSELECT\b</p><p>	// Must access selectedIndex to make default options select</p><p>The following comment/snippet was identified via the pattern: \bFROM\b</p><p>			// Avoid an infinite loop by temporarily removing this function from the getter</p><p>The following comment/snippet was identified via the pattern: \bSELECT\b</p><p>var rfocusable = /^(?:input|select|textarea|button)$/i,</p><p>The following comment/snippet was identified via the pattern: \bWHERE\b</p><p>				// Handle cases where value is null/undef or number</p><p>The following comment/snippet was identified via the pattern: \bSELECT\b</p><p>		select: {</p><p>The following comment/snippet was identified via the pattern: \bSELECT\b</p><p>					one = elem.type === "select-one",</p><p>The following comment/snippet was identified via the pattern: \bWHERE\b</p><p>				// Don't do default actions on window, that's where global variables be (#6170)</p><p>The following comment/snippet was identified via the pattern: \bBUGS\b</p><p>// Related ticket - https://bugs.chromium.org/p/chromium/issues/detail?id=449857</p><p>The following comment/snippet was identified via the pattern: \bSELECT\b</p><p>	rsubmittable = /^(?:input|select|textarea|keygen)/i;</p><p>The following comment/snippet was identified via the pattern: \bQUERY\b</p><p>// key/values into a query string</p><p>The following comment/snippet was identified via the pattern: \bFROM\b</p><p>			// Convert response if prev dataType is non-auto and differs from current</p><p>The following comment/snippet was identified via the pattern: \bFROM\b</p><p>								error: conv ? e : "No conversion from " + prev + " to " + current</p><p>The following comment/snippet was identified via the pattern: \bUSERNAME\b</p><p>		username: null,</p><p>The following comment/snippet was identified via the pattern: \bLATER\b</p><p>		// and/or If-None-Match header later on</p><p>The following comment/snippet was identified via the pattern: \bFROM\b</p><p>				// Extract error from statusText and normalize for non-aborts</p><p>The following comment/snippet was identified via the pattern: \bUSER\b</p><p>		// Make this explicit, since user can override this through ajaxSetup (#11264)</p><p>The following comment/snippet was identified via the pattern: \bUSERNAME\b</p><p>					options.username,</p><p>The following comment/snippet was identified via the pattern: \bBUGS\b</p><p>// https://bugs.webkit.org/show_bug.cgi?id=137337</p><p>The following comment/snippet was identified via the pattern: \bFROM\b</p><p>		// Stop scripts or inline event handlers from being executed immediately</p><p>The following comment/snippet was identified via the pattern: \bUSER\b</p><p>			// user can override it through ajaxSetup method</p><p>The following comment/snippet was identified via the pattern: \bFROM\b</p><p>		// position:fixed elements are offset from the viewport, which itself always has zero offset</p><p>The following comment/snippet was identified via the pattern: \bBUG\b</p><p>// Webkit bug: https://bugs.webkit.org/show_bug.cgi?id=29084</p><p>The following comment/snippet was identified via the pattern: \bBUG\b</p><p>// Blink bug: https://bugs.chromium.org/p/chromium/issues/detail?id=589347</p><p>The following comment/snippet was identified via the pattern: \bSELECT\b</p><p>	"change select submit keydown keypress keyup contextmenu" ).split( " " ),</p><p>The following comment/snippet was identified via the pattern: \bFROM\b</p><p>// derived from file names, and jQuery is normally delivered in a lowercase</p><p></p>
  
### Reference
* 

  
#### CWE Id : 200
  
#### WASC Id : 13
  
#### Source ID : 3
