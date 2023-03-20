import React from "react";
import { Route, Routes } from "react-router-dom";
import Login2 from "./component/login/login";
import HomePageContainer from "./container/homePage/homePageContainer";
import LoginContainer from "./container/login/loginContainer";

const AppRouting = () => {
  //code for protected routing before user authentication and token generation

  // const history = createMemoryHistory();
  console.log("App routing ...");

  let routes = (
    // <BrowserRouter>
    <Routes>
      <Route path="/" element={<HomePageContainer/>} />
      <Route path="/login" element={<LoginContainer/>} />
    </Routes>
    // </BrowserRouter>
  );
  return <React.Fragment>{routes}</React.Fragment>;
};
export default AppRouting;
