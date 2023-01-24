import React from 'react';
import { Modal, Form, Button } from 'react-bootstrap';
import { useForm } from 'react-hook-form';
export default function CreateCategoryModal({
  show,
  handleClose,
  postCreateCategory,
}) {
  const { register, handleSubmit, resetField } = useForm();

  const onSubmit = async (data) => {
    if (!data) return {};
    const convertedData = {
      name: data.name,
      type: parseInt(data.type),
      color: data.color,
    };
    console.log(convertedData);
    resetField('name');
    resetField('color');
    resetField('type');
    postCreateCategory(convertedData);
    handleClose();
  };

  return (
    <Modal show={show} onHide={handleClose}>
      <form onSubmit={handleSubmit(onSubmit)}>
        <Modal.Header closeButton>
          <Modal.Title>Create Category</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form.Control
            className="mb-3"
            type="text"
            {...register('name')}
            placeholder="Name"
          />
          <Form.Select {...register('type')} className="mb-3">
            <option>Choose Category Type</option>
            <option value="0">Expenditures</option>
            <option value="1">Income</option>
          </Form.Select>

          <Form.Control
            className="mb-3"
            type="color"
            {...register('color')}
            placeholder="Color"
          />
          <Button variant="primary" type="submit">
            Create
          </Button>
        </Modal.Body>
      </form>
    </Modal>
  );
}
