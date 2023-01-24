import React, { useEffect, useState } from 'react';
import BarCategory from './BarCategory';
import PaginationContainer from '../PaginationContainer';
import { FaSearch } from 'react-icons/fa';

export default function Category({
  transactionMonthArray,
  currentCategory,
  getTransactionsByCategory,
  transactionList,
  categoryHasItems,
  setQueryTransactionsByCategory,
  queryTransactionsByCategory,
  currentUser,
  getTransactionsArrayByCategory,
}) {
  const [queryChanged, setQueryChanged] = useState(false);
  const [searchInput, setSearchInput] = useState(null);
  const { dataFromServer, pagesCount } = transactionList;

  useEffect(() => {
    if (currentCategory !== null && currentCategory !== undefined)
      getTransactionsByCategory(
        currentCategory.id,
        queryTransactionsByCategory
      );
    if (dataFromServer !== null && dataFromServer !== undefined)
      generateDate(dataFromServer);
  }, [queryChanged, currentUser]);
  useEffect(() => {
    getTransactionsArrayByCategory(currentCategory.id);
  }, [currentCategory, currentUser]);

  function changeQuery(e) {
    if (e === 'Date') queryTransactionsByCategory.sortBy = 'PutTime';
    if (e === 'Amount') queryTransactionsByCategory.sortBy = 'Amount';

    queryTransactionsByCategory.sortDirection == 'ASC'
      ? (queryTransactionsByCategory.sortDirection = 'DESC')
      : (queryTransactionsByCategory.sortDirection = 'ASC');
    setQueryTransactionsByCategory(queryTransactionsByCategory);
    setQueryChanged(!queryChanged);
  }
  const handleSearchChange = (e) => {
    setSearchInput(e.target.value);
    queryTransactionsByCategory.search = e.target.value;
    queryTransactionsByCategory.page = 1;
    setQueryTransactionsByCategory(queryTransactionsByCategory);
    setQueryChanged(!queryChanged);
  };
  function generateDate(dataFromServer) {
    dataFromServer.map((transaction) => {
      var newDate = new Date(transaction.putTime);
      transaction.putTime = newDate.getDate();
    });
  }
  function regenerateDate(putTime) {
    var newDate = new Date(putTime);
    const day =
      newDate.getDate().toString() +
      '-' +
      (newDate.getMonth() + 1).toString() +
      '-' +
      newDate.getUTCFullYear().toString();
    return day;
  }

  return (
    <>
      {categoryHasItems && (
        <BarCategory
          transactionMonthArray={transactionMonthArray}
          category={currentCategory}
          hasItems={categoryHasItems}
        />
      )}
      <table className="table" style={{ margin: '20px' }}>
        <thead>
          <tr>
            <th scope="col">Name</th>
            <th scope="col" onClick={(e) => changeQuery(e.target.innerHTML)}>
              Amount
            </th>
            <th scope="col" onClick={(e) => changeQuery(e.target.innerHTML)}>
              Date
            </th>
            <th scope="col">Description</th>
          </tr>
        </thead>
        <tbody>
          {categoryHasItems && Array.isArray(dataFromServer) ? (
            dataFromServer.map((transaction) => (
              <tr scope="row" key={transaction.id}>
                <td>{transaction.name}</td>
                <td>{transaction.amount}</td>
                <td>{regenerateDate(transaction.putTime)}</td>
                <td>{transaction.description}</td>
              </tr>
            ))
          ) : (
            <tr>
              <td>--Loading--</td>
            </tr>
          )}
        </tbody>
      </table>
      {categoryHasItems && (
        <>
          <div className="d-flex flex-row" style={{ marginLeft: '20px' }}>
            <input
              style={{ maxHeight: '35px' }}
              className="p-2"
              type="text"
              placeholder="Search here"
              onChange={handleSearchChange}
              value={searchInput}
            />
            <PaginationContainer
              pagesCount={pagesCount}
              query={queryTransactionsByCategory}
              setQueryChanged={() => setQueryChanged(!queryChanged)}
            />
          </div>
        </>
      )}
    </>
  );
}
