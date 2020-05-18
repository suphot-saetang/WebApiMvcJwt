WebApiMvcJwt
=============================

Asp.net API + JWT\
Asp.net MVC 5

JWT
```
<base64-encoded header>.<base64-encoded claims>.<base64-encoded signature>
```

A JWT token has tree sections:
```
Header: JSON format which is encoded in Base64
Claims: JSON format which is encoded in Base64.
Signature: Created and signed base on Header and Claims which is encoded in Base64.
```

Route
```
Account/validlogin?userName=admin&userPassword=admin\
Account/GetEmployee
```

