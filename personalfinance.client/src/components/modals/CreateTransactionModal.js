import { useForm } from 'react-hook-form';
import { Modal, Form, Button } from 'react-bootstrap';
import React from 'react';

export default function CreateTransactionModal({
  show,
  handleClose,
  categories,
  postCreateTransaction,
}) {
  const { register, handleSubmit, resetField } = useForm();
  const onSubmit = async (data) => {
    if (!data) return {};
    const convertedData = {
      name: data.name,
      categoryName: data.category,
      amount: parseInt(data.amount),
      description: data.description,
    };
    console.log(convertedData);
    resetField('name');
    resetField('amount');
    resetField('description');
    postCreateTransaction(convertedData);
    handleClose();
  };
  return (
    <>
      <Modal show={show} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>Create Transaction</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <form id="form" onSubmit={handleSubmit(onSubmit)}>
            <Form.Control
              className="mb-3"
              type="text"
              {...register('name')}
              placeholder="Name"
            />

            <Form.Select {...register('category')} className="mb-3">
              <option>Choose Category</option>

              {categories.map((category) => (
                <option key={category.id} value={category.name}>
                  {category.name}
                </option>
              ))}
            </Form.Select>

            <Form.Control
              className="mb-3"
              type="text"
              {...register('description')}
              placeholder="Description"
            />

            <Form.Control
              className="mb-3"
              type="number"
              min={0}
              step={1}
              {...register('amount')}
              placeholder="Amount"
            />
            <Button variant="primary" type="submit">
              Create
            </Button>
          </form>
        </Modal.Body>
      </Modal>
    </>
  );
}
