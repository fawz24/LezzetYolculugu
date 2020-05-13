
# ZAP Scanning Report




## Summary of Alerts

| Risk Level | Number of Alerts |
| --- | --- |
| High | 4 |
| Medium | 2 |
| Low | 2 |
| Informational | 3 |

## Alert Detail


  
  
  
### Remote File Inclusion
##### High (Medium)
  
  
  
  
#### Description
<p>Remote File Include (RFI) is an attack technique used to exploit "dynamic file include" mechanisms in web applications. When web applications take user input (URL, parameter value, etc.) and pass them into file include commands, the web application might be tricked into including remote files with malicious code.</p><p></p><p>Almost all web application frameworks support file inclusion. File inclusion is mainly used for packaging common code into separate files that are later referenced by main application modules. When a web application references an include file, the code in this file may be executed implicitly or explicitly by calling specific procedures. If the choice of module to load is based on elements from the HTTP request, the web application might be vulnerable to RFI.</p><p>An attacker can use RFI for:</p><p>    * Running malicious code on the server: any code in the included malicious files will be run by the server. If the file include is not executed using some wrapper, code in include files is executed in the context of the server user. This could lead to a complete system compromise.</p><p>    * Running malicious code on clients: the attacker's malicious code can manipulate the content of the response sent to the client. The attacker can embed malicious code in the response that will be run by the client (for example, JavaScript to steal the client session cookies).</p><p></p><p>PHP is particularly vulnerable to RFI attacks due to the extensive use of "file includes" in PHP programming and due to default server configurations that increase susceptibility to an RFI attack.</p>
  
  
  
* URL: [http://localhost:55131/Account/SignIn?ReturnUrl=http%3A%2F%2Fwww.google.com%2F](http://localhost:55131/Account/SignIn?ReturnUrl=http%3A%2F%2Fwww.google.com%2F)
  
  
  * Method: `POST`
  
  
  * Parameter: `ReturnUrl`
  
  
  * Attack: `http://www.google.com/`
  
  
  * Evidence: `<title>Google</title>`
  
  
  
  
Instances: 1
  
### Solution
<p>Phase: Architecture and Design</p><p>When the set of acceptable objects, such as filenames or URLs, is limited or known, create a mapping from a set of fixed input values (such as numeric IDs) to the actual filenames or URLs, and reject all other inputs.</p><p>For example, ID 1 could map to "inbox.txt" and ID 2 could map to "profile.txt". Features such as the ESAPI AccessReferenceMap provide this capability.</p><p></p><p>Phases: Architecture and Design; Operation</p><p>Run your code in a "jail" or similar sandbox environment that enforces strict boundaries between the process and the operating system. This may effectively restrict which files can be accessed in a particular directory or which commands can be executed by your software.</p><p>OS-level examples include the Unix chroot jail, AppArmor, and SELinux. In general, managed code may provide some protection. For example, java.io.FilePermission in the Java SecurityManager allows you to specify restrictions on file operations.</p><p>This may not be a feasible solution, and it only limits the impact to the operating system; the rest of your application may still be subject to compromise.</p><p>Be careful to avoid CWE-243 and other weaknesses related to jails.</p><p>For PHP, the interpreter offers restrictions such as open basedir or safe mode which can make it more difficult for an attacker to escape out of the application. Also consider Suhosin, a hardened PHP extension, which includes various options that disable some of the more dangerous PHP features.</p><p></p><p>Phase: Implementation</p><p>Assume all input is malicious. Use an "accept known good" input validation strategy, i.e., use a whitelist of acceptable inputs that strictly conform to specifications. Reject any input that does not strictly conform to specifications, or transform it into something that does. Do not rely exclusively on looking for malicious or malformed inputs (i.e., do not rely on a blacklist). However, blacklists can be useful for detecting potential attacks or determining which inputs are so malformed that they should be rejected outright.</p><p>When performing input validation, consider all potentially relevant properties, including length, type of input, the full range of acceptable values, missing or extra inputs, syntax, consistency across related fields, and conformance to business rules. As an example of business rule logic, "boat" may be syntactically valid because it only contains alphanumeric characters, but it is not valid if you are expecting colors such as "red" or "blue."</p><p>For filenames, use stringent whitelists that limit the character set to be used. If feasible, only allow a single "." character in the filename to avoid weaknesses such as CWE-23, and exclude directory separators such as "/" to avoid CWE-36. Use a whitelist of allowable file extensions, which will help to avoid CWE-434.</p><p></p><p>Phases: Architecture and Design; Operation</p><p>Store library, include, and utility files outside of the web document root, if possible. Otherwise, store them in a separate directory and use the web server's access control capabilities to prevent attackers from directly requesting them. One common practice is to define a fixed constant in each calling program, then check for the existence of the constant in the library/include file; if the constant does not exist, then the file was directly requested, and it can exit immediately.</p><p>This significantly reduces the chance of an attacker being able to bypass any protection mechanisms that are in the base program but not in the include files. It will also reduce your attack surface.</p><p></p><p>Phases: Architecture and Design; Implementation</p><p>Understand all the potential areas where untrusted inputs can enter your software: parameters or arguments, cookies, anything read from the network, environment variables, reverse DNS lookups, query results, request headers, URL components, e-mail, files, databases, and any external systems that provide data to the application. Remember that such inputs may be obtained indirectly through API calls.</p><p>Many file inclusion problems occur because the programmer assumed that certain inputs could not be modified, especially for cookies and URL components.</p>
  
### Reference
* http://projects.webappsec.org/Remote-File-Inclusion
* http://cwe.mitre.org/data/definitions/98.html

  
#### CWE Id : 98
  
#### WASC Id : 5
  
#### Source ID : 1

  
  
  
### SQL Injection
##### High (Medium)
  
  
  
  
#### Description
<p>SQL injection may be possible.</p>
  
  
  
* URL: [http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F3](http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F3)
  
  
  * Method: `POST`
  
  
  * Parameter: `Password`
  
  
  * Attack: `ZAP' AND '1'='1' -- `
  
  
  
  
Instances: 1
  
### Solution
<p>Do not trust client side input, even if there is client side validation in place.  </p><p>In general, type check all data on the server side.</p><p>If the application uses JDBC, use PreparedStatement or CallableStatement, with parameters passed by '?'</p><p>If the application uses ASP, use ADO Command Objects with strong type checking and parameterized queries.</p><p>If database Stored Procedures can be used, use them.</p><p>Do *not* concatenate strings into queries in the stored procedure, or use 'exec', 'exec immediate', or equivalent functionality!</p><p>Do not create dynamic SQL queries using simple string concatenation.</p><p>Escape all data received from the client.</p><p>Apply a 'whitelist' of allowed characters, or a 'blacklist' of disallowed characters in user input.</p><p>Apply the principle of least privilege by using the least privileged database user possible.</p><p>In particular, avoid using the 'sa' or 'db-owner' database users. This does not eliminate SQL injection, but minimizes its impact.</p><p>Grant the minimum database access that is necessary for the application.</p>
  
### Other information
<p>The page results were successfully manipulated using the boolean conditions [ZAP' AND '1'='1' -- ] and [ZAP' AND '1'='2' -- ]</p><p>The parameter value being modified was NOT stripped from the HTML output for the purposes of the comparison</p><p>Data was returned for the original parameter.</p><p>The vulnerability was detected by successfully restricting the data originally returned, by manipulating the parameter</p>
  
### Reference
* https://www.owasp.org/index.php/Top_10_2010-A1
* https://www.owasp.org/index.php/SQL_Injection_Prevention_Cheat_Sheet

  
#### CWE Id : 89
  
#### WASC Id : 19
  
#### Source ID : 1

  
  
  
### External Redirect
##### High (Medium)
  
  
  
  
#### Description
<p>URL redirectors represent common functionality employed by web sites to forward an incoming request to an alternate resource. This can be done for a variety of reasons and is often done to allow resources to be moved within the directory structure and to avoid breaking functionality for users that request the resource at its previous location. URL redirectors may also be used to implement load balancing, leveraging abbreviated URLs or recording outgoing links. It is this last implementation which is often used in phishing attacks as described in the example below. URL redirectors do not necessarily represent a direct security vulnerability but can be abused by attackers trying to social engineer victims into believing that they are navigating to a site other than the true destination.</p>
  
  
  
* URL: [http://localhost:55131/Account/SignIn?ReturnUrl=7144832614552288230.owasp.org](http://localhost:55131/Account/SignIn?ReturnUrl=7144832614552288230.owasp.org)
  
  
  * Method: `POST`
  
  
  * Parameter: `ReturnUrl`
  
  
  * Attack: `7144832614552288230.owasp.org`
  
  
  * Evidence: `7144832614552288230.owasp.org`
  
  
  
  
Instances: 1
  
### Solution
<p>Assume all input is malicious. Use an "accept known good" input validation strategy, i.e., use a whitelist of acceptable inputs that strictly conform to specifications. Reject any input that does not strictly conform to specifications, or transform it into something that does. Do not rely exclusively on looking for malicious or malformed inputs (i.e., do not rely on a blacklist). However, blacklists can be useful for detecting potential attacks or determining which inputs are so malformed that they should be rejected outright.</p><p></p><p>When performing input validation, consider all potentially relevant properties, including length, type of input, the full range of acceptable values, missing or extra inputs, syntax, consistency across related fields, and conformance to business rules. As an example of business rule logic, "boat" may be syntactically valid because it only contains alphanumeric characters, but it is not valid if you are expecting colors such as "red" or "blue."</p><p></p><p>Use a whitelist of approved URLs or domains to be used for redirection.</p><p></p><p>Use an intermediate disclaimer page that provides the user with a clear warning that they are leaving your site. Implement a long timeout before the redirect occurs, or force the user to click on the link. Be careful to avoid XSS problems when generating the disclaimer page.</p><p></p><p>When the set of acceptable objects, such as filenames or URLs, is limited or known, create a mapping from a set of fixed input values (such as numeric IDs) to the actual filenames or URLs, and reject all other inputs.</p><p></p><p>For example, ID 1 could map to "/login.asp" and ID 2 could map to "http://www.example.com/". Features such as the ESAPI AccessReferenceMap provide this capability.</p><p></p><p>Understand all the potential areas where untrusted inputs can enter your software: parameters or arguments, cookies, anything read from the network, environment variables, reverse DNS lookups, query results, request headers, URL components, e-mail, files, databases, and any external systems that provide data to the application. Remember that such inputs may be obtained indirectly through API calls.</p><p></p><p>Many open redirect problems occur because the programmer assumed that certain inputs could not be modified, such as cookies and hidden form fields.</p>
  
### Other information
<p>The response contains a redirect in its Location header which allows an external Url to be set.</p>
  
### Reference
* http://projects.webappsec.org/URL-Redirector-Abuse
* http://cwe.mitre.org/data/definitions/601.html

  
#### CWE Id : 601
  
#### WASC Id : 38
  
#### Source ID : 1

  
  
  
### Path Traversal
##### High (Medium)
  
  
  
  
#### Description
<p>The Path Traversal attack technique allows an attacker access to files, directories, and commands that potentially reside outside the web document root directory. An attacker may manipulate a URL in such a way that the web site will execute or reveal the contents of arbitrary files anywhere on the web server. Any device that exposes an HTTP-based interface is potentially vulnerable to Path Traversal.</p><p></p><p>Most web sites restrict user access to a specific portion of the file-system, typically called the "web document root" or "CGI root" directory. These directories contain the files intended for user access and the executable necessary to drive web application functionality. To access files or execute commands anywhere on the file-system, Path Traversal attacks will utilize the ability of special-characters sequences.</p><p></p><p>The most basic Path Traversal attack uses the "../" special-character sequence to alter the resource location requested in the URL. Although most popular web servers will prevent this technique from escaping the web document root, alternate encodings of the "../" sequence may help bypass the security filters. These method variations include valid and invalid Unicode-encoding ("..%u2216" or "..%c0%af") of the forward slash character, backslash characters ("..\") on Windows-based servers, URL encoded characters "%2e%2e%2f"), and double URL encoding ("..%255c") of the backslash character.</p><p></p><p>Even if the web server properly restricts Path Traversal attempts in the URL path, a web application itself may still be vulnerable due to improper handling of user-supplied input. This is a common problem of web applications that use template mechanisms or load static text from files. In variations of the attack, the original URL parameter value is substituted with the file name of one of the web application's dynamic scripts. Consequently, the results can reveal source code because the file is interpreted as text instead of an executable script. These techniques often employ additional special characters such as the dot (".") to reveal the listing of the current working directory, or "%00" NULL characters in order to bypass rudimentary file extension checks.</p>
  
  
  
* URL: [http://localhost:55131/Account/SignIn?ReturnUrl=SignIn](http://localhost:55131/Account/SignIn?ReturnUrl=SignIn)
  
  
  * Method: `POST`
  
  
  * Parameter: `ReturnUrl`
  
  
  * Attack: `SignIn`
  
  
  
  
Instances: 1
  
### Solution
<p>Assume all input is malicious. Use an "accept known good" input validation strategy, i.e., use a whitelist of acceptable inputs that strictly conform to specifications. Reject any input that does not strictly conform to specifications, or transform it into something that does. Do not rely exclusively on looking for malicious or malformed inputs (i.e., do not rely on a blacklist). However, blacklists can be useful for detecting potential attacks or determining which inputs are so malformed that they should be rejected outright.</p><p></p><p>When performing input validation, consider all potentially relevant properties, including length, type of input, the full range of acceptable values, missing or extra inputs, syntax, consistency across related fields, and conformance to business rules. As an example of business rule logic, "boat" may be syntactically valid because it only contains alphanumeric characters, but it is not valid if you are expecting colors such as "red" or "blue."</p><p></p><p>For filenames, use stringent whitelists that limit the character set to be used. If feasible, only allow a single "." character in the filename to avoid weaknesses, and exclude directory separators such as "/". Use a whitelist of allowable file extensions.</p><p></p><p>Warning: if you attempt to cleanse your data, then do so that the end result is not in the form that can be dangerous. A sanitizing mechanism can remove characters such as '.' and ';' which may be required for some exploits. An attacker can try to fool the sanitizing mechanism into "cleaning" data into a dangerous form. Suppose the attacker injects a '.' inside a filename (e.g. "sensi.tiveFile") and the sanitizing mechanism removes the character resulting in the valid filename, "sensitiveFile". If the input data are now assumed to be safe, then the file may be compromised. </p><p></p><p>Inputs should be decoded and canonicalized to the application's current internal representation before being validated. Make sure that your application does not decode the same input twice. Such errors could be used to bypass whitelist schemes by introducing dangerous inputs after they have been checked.</p><p></p><p>Use a built-in path canonicalization function (such as realpath() in C) that produces the canonical version of the pathname, which effectively removes ".." sequences and symbolic links.</p><p></p><p>Run your code using the lowest privileges that are required to accomplish the necessary tasks. If possible, create isolated accounts with limited privileges that are only used for a single task. That way, a successful attack will not immediately give the attacker access to the rest of the software or its environment. For example, database applications rarely need to run as the database administrator, especially in day-to-day operations.</p><p></p><p>When the set of acceptable objects, such as filenames or URLs, is limited or known, create a mapping from a set of fixed input values (such as numeric IDs) to the actual filenames or URLs, and reject all other inputs.</p><p></p><p>Run your code in a "jail" or similar sandbox environment that enforces strict boundaries between the process and the operating system. This may effectively restrict which files can be accessed in a particular directory or which commands can be executed by your software.</p><p></p><p>OS-level examples include the Unix chroot jail, AppArmor, and SELinux. In general, managed code may provide some protection. For example, java.io.FilePermission in the Java SecurityManager allows you to specify restrictions on file operations.</p><p></p><p>This may not be a feasible solution, and it only limits the impact to the operating system; the rest of your application may still be subject to compromise.</p>
  
### Reference
* http://projects.webappsec.org/Path-Traversal
* http://cwe.mitre.org/data/definitions/22.html

  
#### CWE Id : 22
  
#### WASC Id : 33
  
#### Source ID : 1

  
  
  
### X-Frame-Options Header Not Set
##### Medium (Medium)
  
  
  
  
#### Description
<p>X-Frame-Options header is not included in the HTTP response to protect against 'ClickJacking' attacks.</p>
  
  
  
* URL: [http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F4](http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F4)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
  
* URL: [http://localhost:55131/](http://localhost:55131/)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
  
* URL: [http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F3](http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F3)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
  
* URL: [http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F5](http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F5)
  
  
  * Method: `POST`
  
  
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
  
  
  
  
* URL: [http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F6](http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F6)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
  
* URL: [http://localhost:55131/Recipe/Details/6](http://localhost:55131/Recipe/Details/6)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
  
* URL: [http://localhost:55131/Account/SignIn?ReturnUrl=%2F](http://localhost:55131/Account/SignIn?ReturnUrl=%2F)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
  
* URL: [http://localhost:55131/Account/SignIn?ReturnUrl=%2F](http://localhost:55131/Account/SignIn?ReturnUrl=%2F)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
  
* URL: [http://localhost:55131/Recipe/Index?id=1](http://localhost:55131/Recipe/Index?id=1)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
  
* URL: [http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F5](http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F5)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
  
* URL: [http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F7](http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F7)
  
  
  * Method: `POST`
  
  
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
  
  
  
  
* URL: [http://localhost:55131/Account/SignIn](http://localhost:55131/Account/SignIn)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
  
* URL: [http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F7](http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F7)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Frame-Options`
  
  
  
  
Instances: 25
  
### Solution
<p>Most modern Web browsers support the X-Frame-Options HTTP header. Ensure it's set on all web pages returned by your site (if you expect the page to be framed only by pages on your server (e.g. it's part of a FRAMESET) then you'll want to use SAMEORIGIN, otherwise if you never expect the page to be framed, you should use DENY. ALLOW-FROM allows specific websites to frame the web page in supported web browsers).</p>
  
### Reference
* https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-Frame-Options

  
#### CWE Id : 16
  
#### WASC Id : 15
  
#### Source ID : 3

  
  
  
### Format String Error
##### Medium (Medium)
  
  
  
  
#### Description
<p>A Format String error occurs when the submitted data of an input string is evaluated as a command by the application. </p>
  
  
  
* URL: [http://localhost:55131/Account/SignIn?ReturnUrl=ZAP%25n%25s%25n%25s%25n%25s%25n%25s%25n%25s%25n%25s%25n%25s%25n%25s%25n%25s%25n%25s%25n%25s%25n%25s%25n%25s%25n%25s%25n%25s%25n%25s%25n%25s%25n%25s%25n%25s%25n%25s%0A](http://localhost:55131/Account/SignIn?ReturnUrl=ZAP%25n%25s%25n%25s%25n%25s%25n%25s%25n%25s%25n%25s%25n%25s%25n%25s%25n%25s%25n%25s%25n%25s%25n%25s%25n%25s%25n%25s%25n%25s%25n%25s%25n%25s%25n%25s%25n%25s%25n%25s%0A)
  
  
  * Method: `POST`
  
  
  * Parameter: `ReturnUrl`
  
  
  * Attack: `ZAP%n%s%n%s%n%s%n%s%n%s%n%s%n%s%n%s%n%s%n%s%n%s%n%s%n%s%n%s%n%s%n%s%n%s%n%s%n%s%n%s
`
  
  
  
  
Instances: 1
  
### Solution
<p>Rewrite the background program using proper deletion of bad character strings.  This will require a recompile of the background executable.</p>
  
### Other information
<p>Potential Format String Error.  The script closed the connection on a /%s</p>
  
### Reference
* https://www.owasp.org/index.php/Format_string_attack

  
#### CWE Id : 134
  
#### WASC Id : 6
  
#### Source ID : 1

  
  
  
### X-Content-Type-Options Header Missing
##### Low (Medium)
  
  
  
  
#### Description
<p>The Anti-MIME-Sniffing header X-Content-Type-Options was not set to 'nosniff'. This allows older versions of Internet Explorer and Chrome to perform MIME-sniffing on the response body, potentially causing the response body to be interpreted and displayed as a content type other than the declared content type. Current (early 2014) and legacy versions of Firefox will use the declared content type (if one is set), rather than performing MIME-sniffing.</p>
  
  
  
* URL: [http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F3](http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F3)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
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
  
  
  
  
* URL: [http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F4](http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F4)
  
  
  * Method: `POST`
  
  
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
  
  
  
  
* URL: [http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F7](http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F7)
  
  
  * Method: `POST`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F6](http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F6)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
* URL: [http://localhost:55131/Account/SignIn?ReturnUrl=%2F](http://localhost:55131/Account/SignIn?ReturnUrl=%2F)
  
  
  * Method: `GET`
  
  
  * Parameter: `X-Content-Type-Options`
  
  
  
  
Instances: 36
  
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
  
  
  
  
* URL: [http://localhost:55131/robots.txt](http://localhost:55131/robots.txt)
  
  
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
  
  
  
  
* URL: [http://localhost:55131/lib/jquery/dist/jquery.js](http://localhost:55131/lib/jquery/dist/jquery.js)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F6](http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F6)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [http://localhost:55131/Recipe/Index](http://localhost:55131/Recipe/Index)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F7](http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F7)
  
  
  * Method: `POST`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F7](http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F7)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [http://localhost:55131/sitemap.xml](http://localhost:55131/sitemap.xml)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [http://localhost:55131/Recipe/Details/5%23comment-$%7Bid%7D](http://localhost:55131/Recipe/Details/5%23comment-$%7Bid%7D)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [http://localhost:55131/Recipe/Details/7](http://localhost:55131/Recipe/Details/7)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [http://localhost:55131/Account/SignIn?ReturnUrl=%2F](http://localhost:55131/Account/SignIn?ReturnUrl=%2F)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [http://localhost:55131/api/recipes/4/comments](http://localhost:55131/api/recipes/4/comments)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [http://localhost:55131/favicon.ico](http://localhost:55131/favicon.ico)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [http://localhost:55131/Recipe/Index?id=0](http://localhost:55131/Recipe/Index?id=0)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
* URL: [http://localhost:55131/Recipe/Details/6%23comment-$%7Bid%7D](http://localhost:55131/Recipe/Details/6%23comment-$%7Bid%7D)
  
  
  * Method: `GET`
  
  
  * Evidence: `X-Powered-By: ASP.NET`
  
  
  
  
Instances: 43
  
### Solution
<p>Ensure that your web server, application server, load balancer, etc. is configured to suppress "X-Powered-By" headers.</p>
  
### Reference
* http://blogs.msdn.com/b/varunm/archive/2013/04/23/remove-unwanted-http-response-headers.aspx
* http://www.troyhunt.com/2012/02/shhh-dont-let-your-response-headers.html

  
#### CWE Id : 200
  
#### WASC Id : 13
  
#### Source ID : 3

  
  
  
### Loosely Scoped Cookie
##### Informational (Low)
  
  
  
  
#### Description
<p>Cookies can be scoped by domain or path. This check is only concerned with domain scope.The domain scope applied to a cookie determines which domains can access it. For example, a cookie can be scoped strictly to a subdomain e.g. www.nottrusted.com, or loosely scoped to a parent domain e.g. nottrusted.com. In the latter case, any subdomain of nottrusted.com can access the cookie. Loosely scoped cookies are common in mega-applications like google.com and live.com. Cookies set from a subdomain like app.foo.bar are transmitted only to that domain by the browser. However, cookies scoped to a parent-level domain may be transmitted to the parent, or any subdomain of the parent.</p>
  
  
  
* URL: [http://localhost:55131/Recipe/Details/3](http://localhost:55131/Recipe/Details/3)
  
  
  * Method: `GET`
  
  
  
  
* URL: [http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F4](http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F4)
  
  
  * Method: `POST`
  
  
  
  
* URL: [http://localhost:55131/Recipe/Details/4](http://localhost:55131/Recipe/Details/4)
  
  
  * Method: `GET`
  
  
  
  
* URL: [http://localhost:55131/Recipe/Index](http://localhost:55131/Recipe/Index)
  
  
  * Method: `GET`
  
  
  
  
* URL: [http://localhost:55131/Account/SignUp](http://localhost:55131/Account/SignUp)
  
  
  * Method: `GET`
  
  
  
  
* URL: [http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F5](http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F5)
  
  
  * Method: `POST`
  
  
  
  
* URL: [http://localhost:55131/Recipe/Details/5](http://localhost:55131/Recipe/Details/5)
  
  
  * Method: `GET`
  
  
  
  
* URL: [http://localhost:55131/Account/SignUp](http://localhost:55131/Account/SignUp)
  
  
  * Method: `GET`
  
  
  
  
* URL: [http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F3](http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F3)
  
  
  * Method: `POST`
  
  
  
  
* URL: [http://localhost:55131/Recipe/Details/6](http://localhost:55131/Recipe/Details/6)
  
  
  * Method: `GET`
  
  
  
  
* URL: [http://localhost:55131/Recipe/Details/7](http://localhost:55131/Recipe/Details/7)
  
  
  * Method: `GET`
  
  
  
  
* URL: [http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F7](http://localhost:55131/Account/SignIn?ReturnUrl=%2FRecipe%2FDetails%2F7)
  
  
  * Method: `POST`
  
  
  
  
* URL: [http://localhost:55131/Recipe/Index?id=1](http://localhost:55131/Recipe/Index?id=1)
  
  
  * Method: `GET`
  
  
  
  
* URL: [http://localhost:55131/](http://localhost:55131/)
  
  
  * Method: `GET`
  
  
  
  
Instances: 14
  
### Solution
<p>Always scope cookies to a FQDN (Fully Qualified Domain Name).</p>
  
### Other information
<p>The origin domain used for comparison was: </p><p>localhost</p><p>.AspNetCore.Mvc.CookieTempDataProvider=</p><p></p>
  
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
