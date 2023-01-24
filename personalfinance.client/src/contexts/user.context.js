import { createContext, useState, useContext } from 'react';
import { getHttp, postHttp, deleteHttp } from '../utils/fetch';
import { useLocalStorage } from '../utils/useLocalStorage';

export const UserContext = createContext({
  currentUser: {},
  signOutUser: () => {},
  registerUser: () => {},
  postLoginUser: () => {},
  postRegisterUser: () => {},
});
export const UserProvider = ({ children }) => {
  const [currentUser, setCurrentUser] = useLocalStorage('currentUser', []);
  const endpointAccount = 'account';
  const signOutUser = () => {
    setCurrentUser(null);
  };
  async function registerUser(form) {
    const endpoint = endpointAccount + '/register';
    const fetchedData = await postHttp(endpoint, form);
    if (fetchedData.status > 399) {
      return fetchedData.status;
    }
  }
  async function postLoginUser(form) {
    const endpoint = endpointAccount + '/login';
    const fetchedData = await postHttp(endpoint, form);
    if (fetchedData.status > 399) {
      return fetchedData.status;
    }
    setCurrentUser(fetchedData);
  }
  async function postRegisterUser(form) {
    const endpoint = endpointAccount + '/register';
    const fetchedData = await postHttp(endpoint, form);
    if (fetchedData.status > 399) {
      return fetchedData.status;
    }
    // setCurrentUser(fetchedData);
  }
  const value = {
    currentUser,
    signOutUser,
    registerUser,
    postLoginUser,
    postRegisterUser,
  };
  return <UserContext.Provider value={value}>{children}</UserContext.Provider>;
};
