//import "../fake-db";
import "../styles/_app.scss";
/* eslint-disable react-hooks/exhaustive-deps */
import React from "react";
import { Provider } from "react-redux";
import { Router } from "react-router-dom";
import MatxTheme from "./MatxLayout/MatxTheme/MatxTheme";
import AppContext from "./appContext";
import history from "history.js";

import routes from "./RootRoutes";
import { Store } from "./redux/Store";
import Auth from "./auth/Auth";
import MatxLayout from "./MatxLayout/MatxLayoutSFC";
import AuthGuard from "./auth/AuthGuard";

const App = () => {
  return (
    <AppContext.Provider value={{ routes }}>
      <Provider store={Store}>
        <Auth>
          <Router history={history}>
            <AuthGuard>
              <MatxTheme>
                <MatxLayout />
              </MatxTheme>
            </AuthGuard>
          </Router>
        </Auth>
      </Provider>
    </AppContext.Provider>
  );
};

export default App;
