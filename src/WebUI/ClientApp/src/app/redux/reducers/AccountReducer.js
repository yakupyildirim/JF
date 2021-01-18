import {
  REGISTER_SUCCESS,
  REGISTER_ERROR,
} from "../actions/AccountActions";

const initialState = {
  success: false,
  error: {
    username: null,
    email:null,
    password: null
  }
};

const RegisterReducer = function(state = initialState, action) {
  switch (action.type) {
    case REGISTER_SUCCESS: {
      return {
        ...state,
        success: true
      };
    }
    case REGISTER_ERROR: {
      return {
        success: false,
        error: action.data
      };
    }
    default: {
      return state;
    }
  }
};

export default RegisterReducer;
