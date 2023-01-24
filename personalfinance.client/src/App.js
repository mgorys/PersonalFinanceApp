import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import './App.css';
import Navbar from './components/Navbar';
import Authorization from './components/Authorization';
import NotFound from './components/NotFound';
import Home from './components/HomePage/Home';
import { useState, useContext, useEffect } from 'react';
import EditCategoryModal from './components/modals/EditCategoryModal';
import CreateCategoryModal from './components/modals/CreateCategoryModal';
import { DataContext } from './contexts/data.context';
import CreateTransactionModal from './components/modals/CreateTransactionModal';
import Category from './components/CategoryPage/Category';
import { UserContext } from './contexts/user.context';
import AccountLoginModal from './components/modals/AccountLoginModal';

function App() {
  const [showEditCategoryModal, setShowEditCategoryModal] = useState(false);
  const [showCreateCategoryModal, setShowCreateCategoryModal] = useState(false);
  const [showCreateTransactionModal, setShowCreateTransactionModal] =
    useState(false);
  const [showLoginModal, setShowLoginModal] = useState(false);
  const [currentCategoryToChange, setCurrentCategoryToChange] =
    useState(undefined);
  const [currentCategory, setCurrentCategory] = useState(null);
  const [queryTransactionsByCategory, setQueryTransactionsByCategory] =
    useState(null);
  const {
    categoryList,
    hasItems,
    categoryHasItems,
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
  } = useContext(DataContext);
  const {
    postLoginUser,
    currentUser,
    signOutUser,
    registerUser,
    postRegisterUser,
  } = useContext(UserContext);
  useEffect(() => {
    setQueryTransactionsByCategory(defaultQuery);
  }, []);

  return (
    <>
      <Router>
        <Navbar
          registerUser={registerUser}
          signOutUser={signOutUser}
          currentUser={currentUser}
          handleOpenLoginModal={() => setShowLoginModal(true)}
          handleOpenCreateCategoryModal={() => setShowCreateCategoryModal(true)}
          handleOpenCreateTransactionModal={() =>
            setShowCreateTransactionModal(true)
          }
        />
        <Routes>
          <Route
            exact
            path="/"
            element={
              <Home
                currentUser={currentUser}
                setCurrentCategory={setCurrentCategory}
                getCategories={getCategories}
                categoryList={categoryList.dataFromServer}
                hasItems={hasItems}
                deleteCategory={deleteCategory}
                setCurrentCategoryToChange={setCurrentCategoryToChange}
                handleOpenEditCategoryModal={() =>
                  setShowEditCategoryModal(true)
                }
              />
            }
          />
          <Route
            path="category"
            element={
              <Category
                getTransactionsArrayByCategory={getTransactionsArrayByCategory}
                transactionMonthArray={transactionMonthArray.dataFromServer}
                currentUser={currentUser}
                queryTransactionsByCategory={queryTransactionsByCategory}
                setQueryTransactionsByCategory={setQueryTransactionsByCategory}
                categoryHasItems={categoryHasItems}
                currentCategory={currentCategory}
                getTransactionsByCategory={getTransactionsByCategory}
                transactionList={transactionList}
              />
            }
          />
          <Route path="auth" element={<Authorization />} />
          <Route path="*" element={<NotFound />} />
        </Routes>
      </Router>
      {currentCategoryToChange && (
        <EditCategoryModal
          category={currentCategoryToChange}
          show={showEditCategoryModal}
          handleClose={() => setShowEditCategoryModal(false)}
          postEditCategory={postEditCategory}
        />
      )}
      <CreateCategoryModal
        show={showCreateCategoryModal}
        handleClose={() => setShowCreateCategoryModal(false)}
        postCreateCategory={postCreateCategory}
      />
      {hasItems && (
        <CreateTransactionModal
          categories={categoryList.dataFromServer}
          show={showCreateTransactionModal}
          handleClose={() => setShowCreateTransactionModal(false)}
          postCreateTransaction={postCreateTransaction}
        />
      )}
      <AccountLoginModal
        show={showLoginModal}
        handleClose={() => setShowLoginModal(false)}
        postRegisterUser={postRegisterUser}
        postLoginUser={postLoginUser}
      />
    </>
  );
}

export default App;
