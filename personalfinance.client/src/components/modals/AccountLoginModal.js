import React from 'react';
import { Modal, Form, Button } from 'react-bootstrap';
import { useForm } from 'react-hook-form';
export default function AccountLoginModal({
  show,
  handleClose,
  postLoginUser,
  postRegisterUser,
}) {
  const { register, handleSubmit, resetField } = useForm();

  const onSubmitLogin = async (data) => {
    if (!data) return {};
    const logData = {
      email: data.emailLogin,
      password: data.passwordLogin,
    };
    resetField('emailLogin');
    resetField('passwordLogin');
    postLoginUser(logData);
    handleClose();
  };
  const onSubmitRegister = async (data) => {
    if (!data) return {};
    const logData = {
      name: data.nameRegister,
      email: data.emailRegister,
      password: data.passwordRegister,
      confirmPassword: data.confirmPasswordRegister,
    };
    resetField('emailRegister');
    resetField('nameRegister');
    resetField('passwordRegister');
    resetField('confirmPasswordRegister');
    postRegisterUser(logData);
    console.log(logData);
  };

  return (
    <Modal show={show} onHide={handleClose}>
      <form onSubmit={handleSubmit(onSubmitLogin)}>
        <Modal.Header closeButton>
          <Modal.Title>Log in</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form.Control
            className="mb-3"
            type="text"
            {...register('emailLogin')}
            placeholder="E-mail"
          />
          <Form.Control
            className="mb-3"
            type="password"
            {...register('passwordLogin')}
            placeholder="Password"
          />
          <Button variant="primary" type="submit">
            Log in
          </Button>
        </Modal.Body>
      </form>
      <form onSubmit={handleSubmit(onSubmitRegister)}>
        <Modal.Header closeButton>
          <Modal.Title>Register</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form.Control
            className="mb-3"
            type="text"
            {...register('nameRegister')}
            placeholder="Name"
          />
          <Form.Control
            className="mb-3"
            type="text"
            {...register('emailRegister')}
            placeholder="E-mail"
          />
          <Form.Control
            className="mb-3"
            type="password"
            {...register('passwordRegister')}
            placeholder="Password"
          />
          <Form.Control
            className="mb-3"
            type="password"
            {...register('confirmPasswordRegister')}
            placeholder="Password"
          />
          <Button variant="primary" type="submit">
            Register
          </Button>
        </Modal.Body>
      </form>
    </Modal>
  );
}
