import axios from "axios";
import localStorageService from "./localStorageService";
import Qs from "qs";

var Querystring = require('querystring');

const data = {
  grant_type: "password",
  client_id: "panel_api",
  client_secret: "JFPanel",
  scope: "rest_auth",
  username: "yakupyildirim@hotmail.com.tr",
  password: "deneme"
};

class JwtAuthService {

  // Dummy user object just for the demo
  user = {
    userId: "1",
    role: 'ADMIN',
    displayName: "Jason Alexander",
    email: "jasonalexander@gmail.com",
    photoURL: "/assets/images/face-6.jpg",
    age: 25,
    token: "faslkhfh423oiu4h4kj432rkj23h432u49ufjaklj423h4jkhkjh"
  }

  // You need to send http request with email and passsword to your server in this method
  // Your server will return user object & a Token
  // User should have role property
  // You can define roles in app/auth/authRoles.js
  loginWithEmailAndPassword = (email, password) => {
    axios.post("https://localhost:5001/connect/token",
    Querystring.stringify(data))   
    .then(response => {
       console.log(response.data);
       console.log('userresponse ' + response.data.access_token); 
       this.setSession(response.data.access_token);
       // Set user
       this.setUser(response.data);
       return response.data;
     })   
    .catch((error) => {
       console.log('error ' + error);   
    });
  };

  // You need to send http requst with existing token to your server to check token is valid
  // This method is being used when user logged in & app is reloaded
  loginWithToken = () => {
    return new Promise((resolve, reject) => {
      setTimeout(() => {
        resolve(this.user);
      }, 100);
    }).then(data => {
      // Token is valid
      this.setSession(data.token);
      this.setUser(data);
      return data;
    });
  };

  logout = () => {
    this.setSession(null);
    this.removeUser();
  }

  // Set token to all http request header, so you don't need to attach everytime
  setSession = token => {
    if (token) {
      localStorage.setItem("jwt_token", token);
      axios.defaults.headers.common["Authorization"] = "Bearer " + token;
    } else {
      localStorage.removeItem("jwt_token");
      delete axios.defaults.headers.common["Authorization"];
    }
  };

  // Save user to localstorage
  setUser = (user) => {
    localStorageService.setItem("auth_user", user);
  }
  // Remove user from localstorage
  removeUser = () => {
    localStorage.removeItem("auth_user");
  }
}

export default new JwtAuthService();
