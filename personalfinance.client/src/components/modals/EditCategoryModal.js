import React from 'react';
import { Modal, Form, Button } from 'react-bootstrap';
import { useForm } from 'react-hook-form';
import { FaTimes } from 'react-icons/fa';
export default function EditCategoryModal({
  show,
  handleClose,
  category,
  postEditCategory,
}) {
  const { register, handleSubmit, resetField } = useForm();

  const onSubmit = async (data) => {
    if (!data) return {};
    const convertedData = {
      id: category.id,
      name: data.name,
      color: data.color,
    };
    console.log(convertedData);
    postEditCategory(convertedData);
    handleClose();
    resetField('name');
    resetField('color');
  };
  const handleClickClose = () => {
    handleClose();
    resetField('name');
    resetField('color');
  };

  return (
    <Modal show={show} onHide={handleClickClose}>
      <Modal.Header closeButton>
        <Modal.Title>Edit Category</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <form id="form" onSubmit={handleSubmit(onSubmit)}>
          <Form.Control
            type="text"
            defaultValue={category.name ?? ''}
            {...register('name')}
            placeholder="Name"
            className="mb-3"
          />
          <Form.Control
            type="color"
            defaultValue={category.color ?? ''}
            {...register('color')}
            placeholder="Color"
            className="mb-3"
          />
          <Button variant="primary" type="submit">
            Apply Edit
          </Button>
        </form>
      </Modal.Body>
    </Modal>
  );
}
