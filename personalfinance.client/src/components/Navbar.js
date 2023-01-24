import React from 'react';
import { Link } from 'react-router-dom';
import { Button } from 'react-bootstrap';

export default function Navbar({
  handleOpenCreateCategoryModal,
  handleOpenCreateTransactionModal,
  handleOpenLoginModal,
  currentUser,
  signOutUser,
}) {
  function handleClick() {
    console.log(currentUser);
  }
  function handleSignOut() {
    signOutUser();
  }
  return (
    <div className="title-container">
      <Link
        to="/"
        style={{
          fontSize: '25px',
          textDecoration: 'none',
          color: 'white',
          paddingBottom: '10px',
          paddingTop: '10px',
          marginLeft: 'auto',
          marginRight: 'auto',
        }}>
        Personal Finance App
      </Link>
      <div style={{ margin: '10px' }}>
        <Button
          variant="primary"
          style={{ marginRight: '5px' }}
          onClick={() => handleOpenCreateCategoryModal()}>
          Create Category
        </Button>
        <Button
          variant="success"
          style={{ marginRight: '5px' }}
          onClick={() => handleOpenCreateTransactionModal()}>
          Create Transaction
        </Button>
        {currentUser ? (
          <>
            <Button
              variant="warning"
              style={{ marginRight: '5px' }}
              onClick={handleClick}>
              {currentUser.userName}
            </Button>
            <Button variant="danger" onClick={handleSignOut}>
              Sign Out
            </Button>
          </>
        ) : (
          <Button variant="success" onClick={() => handleOpenLoginModal()}>
            Account
          </Button>
        )}
      </div>
    </div>
  );
}
