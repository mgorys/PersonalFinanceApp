import React, { useEffect, useState } from 'react';
import BarCategory from './BarCategory';

export default function Category({
  currentCategory,
  getTransactionsByCategory,
  transactionList,
  categoryHasItems,
  setQueryTransactionsByCategory,
  queryTransactionsByCategory,
  currentUser,
}) {
  const [queryChanged, setQueryChanged] = useState(false);

  useEffect(() => {
    if (currentCategory !== null && currentCategory !== undefined)
      getTransactionsByCategory(
        currentCategory.id,
        queryTransactionsByCategory
      );
    if (transactionList !== null && transactionList !== undefined)
      generateDate(transactionList);
  }, [queryChanged, currentUser]);

  function changeQuery() {
    queryTransactionsByCategory.sortBy = 'PutTime';
    queryTransactionsByCategory.sortDirection == 'ASC'
      ? (queryTransactionsByCategory.sortDirection = 'DESC')
      : (queryTransactionsByCategory.sortDirection = 'ASC');
    setQueryTransactionsByCategory(queryTransactionsByCategory);
    setQueryChanged(!queryChanged);
  }
  function generateDate(transactionList) {
    transactionList.map((transaction) => {
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
          transactionList={transactionList}
          category={currentCategory}
          hasItems={categoryHasItems}
        />
      )}
      <table className="table" style={{ margin: '20px' }}>
        <thead>
          <tr>
            <th scope="col">Name</th>
            <th scope="col">Amount</th>
            <th scope="col" onClick={() => changeQuery()} type="button">
              Date
            </th>
            <th scope="col">Description</th>
          </tr>
        </thead>
        <tbody>
          {categoryHasItems && Array.isArray(transactionList) ? (
            transactionList.map((transaction) => (
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
    </>
  );
}
