import React, { useContext, useState, useEffect } from 'react';
import GraphExpenditure from './GraphExpenditure';
import GraphIncome from './GraphIncome';
import { FaTrashAlt, FaRegEdit } from 'react-icons/fa';
import { Button, Container, Row, Col, Stack } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import ProgressBar from './ProgressBarTotal';

export default function Home({
  handleOpenEditCategoryModal,
  getCategories,
  categoryList,
  hasItems,
  setCurrentCategoryToChange,
  deleteCategory,
  setCurrentCategory,
  currentUser,
}) {
  const [totalIncome, setTotalIncome] = useState(0);
  const [totalExpenditures, setTotalExpenditures] = useState(0);
  function handleClickEditCategory(category) {
    handleOpenEditCategoryModal();
    setCurrentCategoryToChange(category);
  }

  function handleClickDeleteCategory(id) {
    deleteCategory(id);
  }
  function handleLinkClick(category) {
    setCurrentCategory(category);
  }

  useEffect(() => {
    getCategories();
  }, [currentUser]);

  return (
    <>
      <div className="container">
        <div className="row">
          <div className="col-8">
            {hasItems && (
              <GraphExpenditure
                setTotalExpenditures={setTotalExpenditures}
                fetchedData={categoryList}
                hasItems={hasItems}
              />
            )}

            {hasItems && (
              <GraphIncome
                setTotalIncome={setTotalIncome}
                fetchedData={categoryList}
                hasItems={hasItems}
              />
            )}
          </div>
          <div className="col-4">
            {hasItems && (
              <ProgressBar
                totalExpenditures={totalExpenditures}
                totalIncome={totalIncome}
              />
            )}
            <div>
              {hasItems && Array.isArray(categoryList) ? (
                categoryList.map((category) => (
                  <Stack
                    className="border border-dark rounded mx-auto m-2"
                    style={{ maxWidth: '500px' }}
                    direction="horizontal"
                    gap={2}
                    key={category.id}
                    value={category.name}>
                    <div
                      style={{
                        backgroundColor: `${category.color}`,
                        height: '2.5rem',
                        width: '1rem',
                      }}
                    />
                    <Link
                      onClick={() => handleLinkClick(category)}
                      to={'/category'}
                      style={{
                        marginBottom: '0px',
                        textDecoration: 'none',
                        color: 'black',
                      }}>
                      {category.name}
                    </Link>

                    <FaRegEdit
                      role="button"
                      className="ms-auto"
                      onClick={() => handleClickEditCategory(category)}
                    />
                    <FaTrashAlt
                      role="button"
                      onClick={() => handleClickDeleteCategory(category.id)}
                    />
                    <div />
                  </Stack>
                ))
              ) : (
                <div>--Loading--</div>
              )}
            </div>
          </div>
        </div>
      </div>
    </>
  );
}
