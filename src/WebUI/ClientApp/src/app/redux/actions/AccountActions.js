import jwtAuthService from "../../services/jwtAuthService";
import FirebaseAuthService from "../../services/firebase/firebaseAuthService";
import { setUserData } from "./UserActions";
import history from "history.js";
import axios from "axios";

export const REGISTER_ERROR = "REGISTER_ERROR";
export const REGISTER_SUCCESS = "REGISTER_SUCCESS";

export function userRegister({username, email, password}) {
  return dispatch =>
  axios.post("https://localhost:5001/api/user/register", { username, email, password }).then(res => {
    dispatch({
      type: REGISTER_SUCCESS,
      payload: res.data
    });
    history.push({
      pathname: "/session/signin"
    });
  })
  .catch(error => {
    return dispatch({
      type: REGISTER_ERROR,
      payload: error
    });
  });
};
