import { Formik } from 'formik';
import i18n from 'i18n-js';
import { Container, Content, Form, Input, InputGroup, Label } from 'native-base';
import PropTypes from 'prop-types';
import React, { useRef } from 'react';
import * as Yup from 'yup';
import FormButtons from '../../components/FormButtons/FormButtons';
import ValidationMessage from '../../components/ValidationMessage/ValidationMessage';

const ValidationSchema = Yup.object().shape({
  userName: Yup.string().required('RocketAccount::ThisFieldIsRequired.'),
  email: Yup.string()
    .required('RocketAccount::ThisFieldIsRequired.')
    .email('RocketAccount::ThisFieldIsNotAValidEmailAddress.'),
});

function ManageProfileForm({ editingUser = {}, submit, cancel }) {
  const usernameRef = useRef();
  const nameRef = useRef();
  const surnameRef = useRef();
  const emailRef = useRef();
  const phoneNumberRef = useRef();

  const onSubmit = values => {
    submit({
      ...editingUser,
      ...values,
    });
  };

  return (
    <Formik
      enableReinitialize
      validationSchema={ValidationSchema}
      initialValues={{
        ...editingUser,
      }}
      onSubmit={values => onSubmit(values)}>
      {({ handleChange, handleBlur, handleSubmit, values, errors, isValid }) => (
        <>
          <Container>
            <Content px20>
              <Form>
                <InputGroup rocketInputGroup>
                  <Label rocketLabel>{i18n.t('RocketIdentity::UserName')}*</Label>
                  <Input
                    ref={usernameRef}
                    onSubmitEditing={() => nameRef.current._root.focus()}
                    returnKeyType="next"
                    onChangeText={handleChange('userName')}
                    onBlur={handleBlur('userName')}
                    value={values.userName}
                    rocketInput
                  />
                </InputGroup>
                <ValidationMessage>{errors.userName}</ValidationMessage>
                <InputGroup rocketInputGroup>
                  <Label rocketLabel>{i18n.t('RocketIdentity::DisplayName:Name')}</Label>
                  <Input
                    rocketInput
                    ref={nameRef}
                    onSubmitEditing={() => surnameRef.current._root.focus()}
                    returnKeyType="next"
                    onChangeText={handleChange('name')}
                    onBlur={handleBlur('name')}
                    value={values.name}
                  />
                </InputGroup>
                <InputGroup rocketInputGroup>
                  <Label rocketLabel>{i18n.t('RocketIdentity::DisplayName:Surname')}</Label>
                  <Input
                    rocketInput
                    ref={surnameRef}
                    onSubmitEditing={() => phoneNumberRef.current._root.focus()}
                    returnKeyType="next"
                    onChangeText={handleChange('surname')}
                    onBlur={handleBlur('surname')}
                    value={values.surname}
                  />
                </InputGroup>
                <InputGroup rocketInputGroup>
                  <Label rocketLabel>{i18n.t('RocketIdentity::PhoneNumber')}</Label>
                  <Input
                    rocketInput
                    ref={phoneNumberRef}
                    onSubmitEditing={() => emailRef.current._root.focus()}
                    returnKeyType="next"
                    onChangeText={handleChange('phoneNumber')}
                    onBlur={handleBlur('phoneNumber')}
                    value={values.phoneNumber}
                  />
                </InputGroup>
                <InputGroup rocketInputGroup>
                  <Label rocketLabel>{i18n.t('RocketIdentity::EmailAddress')}*</Label>
                  <Input
                    rocketInput
                    ref={emailRef}
                    returnKeyType="done"
                    onSubmitEditing={handleSubmit}
                    onChangeText={handleChange('email')}
                    onBlur={handleBlur('email')}
                    value={values.email}
                  />
                </InputGroup>
                <ValidationMessage>{errors.email}</ValidationMessage>
              </Form>
            </Content>
          </Container>
          <FormButtons submit={handleSubmit} cancel={cancel} isSubmitDisabled={!isValid} />
        </>
      )}
    </Formik>
  );
}

ManageProfileForm.propTypes = {
  editingUser: PropTypes.object.isRequired,
  submit: PropTypes.func.isRequired,
  cancel: PropTypes.func.isRequired,
};

export default ManageProfileForm;
