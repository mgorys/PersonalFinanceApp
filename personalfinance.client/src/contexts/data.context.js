import { createContext, useState, useContext } from 'react';
import { getHttp, postHttp, deleteHttp } from '../utils/fetch';
import { UserContext } from './user.context';

export const DataContext = createContext({
  categoryList: {},
  hasItems: false,
  categoryHasItems: false,
  transactionList: {},
  defaultQuery: {},
  transactionMonthArray: [],
  getCategories: () => {},
  postCreateTransaction: () => {},
  postEditCategory: () => {},
  postCreateCategory: () => {},
  deleteCategory: () => {},
  getTransactionsByCategory: () => {},
  getTransactionsArrayByCategory: () => {},
});
export const DataProvider = ({ children }) => {
  const { currentUser } = useContext(UserContext);
  const [categoryList, setCategoryList] = useState([]);
  const [transactionList, setTransactionList] = useState([]);
  const [transactionMonthArray, setTransactionMonthArray] = useState([]);
  const [hasItems, setHasItems] = useState(false);
  const [categoryHasItems, setCategoryHasItems] = useState(false);
  let endpointCategory = 'category';
  let endpointTransaction = 'transaction';
  let defaultQuery = {
    sortBy: null,
    sortDirection: null,
    page: null,
  };
  const defaultCategories = [
    {
      id: 1,
      name: 'Expenditures',
      color: '#37FF00',
      amount: 1000,
      type: 0,
    },
    {
      id: 2,
      name: 'Income',
      color: '#EE00FF',
      amount: 1200,
      type: 1,
    },
  ];
  const defaultTransactions = [
    {
      amount: 1000,
      categoryId: 1,
      categoryName: 'Expenditures',
      id: 1,
      name: 'Rent',
      color: '#37FF00',
      type: 0,
      putTime: '2023-07-23T13:32:22.4657846',
    },
    {
      amount: 1200,
      categoryId: 2,
      categoryName: 'Income',
      id: 2,
      name: 'Salary',
      color: '#EE00FF',
      type: 1,
      putTime: '2023-07-23T13:32:22.4657846',
    },
  ];

  async function getCategories() {
    if (currentUser === null) {
      setCategoryList(defaultCategories);
      setHasItems(true);
    } else {
      const fetchedData = await getHttp(
        endpointCategory,
        defaultQuery,
        currentUser.token
      );
      if (fetchedData.status > 399) {
        return fetchedData.status;
      }
      setCategoryList(fetchedData);
      setHasItems(true);
    }
  }
  async function getTransactionsByCategory(id, query) {
    if (currentUser == null) {
      setTransactionList(defaultTransactions);
      setCategoryHasItems(true);
    } else {
      const endpoint = endpointTransaction + '/' + id;
      const fetchedData = await getHttp(endpoint, query, currentUser.token);
      if (fetchedData.status > 399) {
        return fetchedData.status;
      }
      setTransactionList(fetchedData);
      setCategoryHasItems(true);
    }
  }
  async function getTransactionsArrayByCategory(id) {
    if (currentUser == null) {
      setTransactionList(defaultTransactions);
      setCategoryHasItems(true);
    } else {
      const endpoint = endpointTransaction + '/bymonth/' + id;
      const fetchedData = await getHttp(endpoint, null, currentUser.token);
      if (fetchedData.status > 399) {
        return fetchedData.status;
      }
      setTransactionMonthArray(fetchedData);
    }
  }
  async function postCreateTransaction(body) {
    const fetchedData = await postHttp(
      endpointTransaction,
      body,
      currentUser.token
    );
    if (fetchedData.status > 399) {
      return fetchedData.status;
    }
    getCategories();
  }
  async function postEditCategory(body) {
    const endpoint = endpointCategory + '/edit';
    const fetchedData = await postHttp(endpoint, body, currentUser.token);
    if (fetchedData.status > 399) {
      return fetchedData.status;
    }
    getCategories();
  }
  async function postCreateCategory(body) {
    const endpoint = endpointCategory + '/create';
    const fetchedData = await postHttp(endpoint, body, currentUser.token);
    if (fetchedData.status > 399) {
      return fetchedData.status;
    }
    getCategories();
  }
  async function deleteCategory(id) {
    const endpoint = endpointCategory + `/${id}`;
    const fetchedData = await deleteHttp(endpoint, currentUser.token);
    if (fetchedData.status > 399) {
      return fetchedData.status;
    }
    getCategories();
  }

  const value = {
    categoryList,
    categoryHasItems,
    hasItems,
    transactionList,
    defaultQuery,
    transactionMonthArray,
    getCategories,
    postCreateTransaction,
    postEditCategory,
    postCreateCategory,
    deleteCategory,
    getTransactionsByCategory,
    getTransactionsArrayByCategory,
  };
  return <DataContext.Provider value={value}>{children}</DataContext.Provider>;
};
