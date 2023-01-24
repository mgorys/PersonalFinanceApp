import React from 'react';
import Pagination from 'react-bootstrap/Pagination';
export default function PaginationContainer({
  pagesCount,
  query,
  setQueryChanged,
}) {
  function handlechangePage(e) {
    if (query.page === parseInt(e)) {
      return;
    } else {
      let page = parseInt(e);
      query.page = page;
      setQueryChanged();
    }
  }
  let items = [];
  for (let number = 1; number <= pagesCount; number++) {
    items.push(
      <Pagination.Item
        key={number}
        active={number === query.page ?? 1}
        onClick={(e) => handlechangePage(e.target.innerHTML)}>
        {number}
      </Pagination.Item>
    );
  }
  return (
    <>
      <div style={{ marginLeft: '20px' }}>
        <Pagination size="sm">{items}</Pagination>
      </div>
    </>
  );
}
