import { Formik } from 'formik';
import i18n from 'i18n-js';
import {
  Body,
  Button,
  CheckBox,
  Container,
  Content,
  Input,
  InputGroup,
  Item,
  Icon,
  Label,
  ListItem,
  Segment,
  Text,
} from 'native-base';
import PropTypes from 'prop-types';
import React, { useRef, useState } from 'react';
import { StyleSheet, TouchableOpacity, View } from 'react-native';
import * as Yup from 'yup';
import FormButtons from '../../components/FormButtons/FormButtons';
import ValidationMessage from '../../components/ValidationMessage/ValidationMessage';
import { usePermission } from '../../hooks/UsePermission';
import UserRoles from './UserRoles';

const validations = {
  userName: Yup.string().required('RocketAccount::ThisFieldIsRequired.'),
  email: Yup.string()
    .email('RocketAccount::ThisFieldIsNotAValidEmailAddress.')
    .required('RocketAccount::ThisFieldIsRequired.'),
};

let roleNames = [];

function onChangeRoles(roles) {
  roleNames = roles;
}

function CreateUpdateUserForm({ editingUser = {}, submit, remove }) {
  const [selectedTab, setSelectedTab] = useState(0);
  const [showPassword, setShowPassword] = useState(false);

  const usernameRef = useRef();
  const nameRef = useRef();
  const surnameRef = useRef();
  const emailRef = useRef();
  const phoneNumberRef = useRef();
  const passwordRef = useRef();

  const hasRemovePermission = usePermission('RocketIdentity.Users.Delete');

  const onSubmit = values => {
    submit({
      ...editingUser,
      ...values,
      roleNames,
    });
  };

  const passwordValidation = Yup.lazy(() => {
    if (editingUser.id) {
      return Yup.string();
    }
    return Yup.string().required('RocketAccount::ThisFieldIsRequired.');
  });

  return (
    <Formik
      enableReinitialize
      validationSchema={Yup.object().shape({
        ...validations,
        password: passwordValidation,
      })}
      initialValues={{
        lockoutEnabled: false,
        twoFactorEnabled: false,
        ...editingUser,
      }}
      onSubmit={values => onSubmit(values)}>
      {({ handleChange, handleBlur, handleSubmit, values, errors, isValid, setFieldValue }) => (
        <>
          <Segment>
            <Button first primary={selectedTab === 0} onPress={() => setSelectedTab(0)}>
              <Text dark light={selectedTab === 0}>
                {i18n.t('RocketIdentity::UserInformations')}
              </Text>
            </Button>
            <Button last primary={selectedTab === 1} onPress={() => setSelectedTab(1)}>
              <Text dark light={selectedTab === 1}>
                {i18n.t('RocketIdentity::Roles')}
              </Text>
            </Button>
          </Segment>
          <Container style={styles.container}>
            <Content px20>
              <View style={{ display: selectedTab === 0 ? 'flex' : 'none' }}>
                <View style={{ alignItems: 'center', margin: 10, zIndex: 1 }} />
                <InputGroup rocketInputGroup>
                  <Label rocketLabel>{i18n.t('RocketIdentity::UserName')}*</Label>
                  <Input
                    rocketInput
                    ref={usernameRef}
                    onSubmitEditing={() => nameRef.current._root.focus()}
                    returnKeyType="next"
                    onChangeText={handleChange('userName')}
                    onBlur={handleBlur('userName')}
                    value={values.userName}
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
                    onSubmitEditing={() => emailRef.current._root.focus()}
                    returnKeyType="next"
                    onChangeText={handleChange('surname')}
                    onBlur={handleBlur('surname')}
                    value={values.surname}
                  />
                </InputGroup>
                <InputGroup rocketInputGroup>
                  <Label rocketLabel>{i18n.t('RocketIdentity::EmailAddress')}*</Label>
                  <Input
                    rocketInput
                    ref={emailRef}
                    onSubmitEditing={() => phoneNumberRef.current._root.focus()}
                    returnKeyType="next"
                    onChangeText={handleChange('email')}
                    onBlur={handleBlur('email')}
                    value={values.email}
                  />
                </InputGroup>
                <ValidationMessage>{errors.email}</ValidationMessage>
                <InputGroup rocketInputGroup>
                  <Label rocketLabel>{i18n.t('RocketIdentity::PhoneNumber')}</Label>
                  <Input
                    rocketInput
                    ref={phoneNumberRef}
                    returnKeyType={!editingUser.id ? 'next' : 'default'}
                    onChangeText={handleChange('phoneNumber')}
                    onBlur={handleBlur('phoneNumber')}
                    value={values.phoneNumber}
                  />
                </InputGroup>
                {!editingUser.id ? (
                  <>
                    <InputGroup rocketInputGroup>
                      <Label rocketLabel>{i18n.t('RocketIdentity::Password')}*</Label>
                      <Item rocketInput>
                        <Input
                          ref={passwordRef}
                          secureTextEntry={!showPassword}
                          onChangeText={handleChange('password')}
                          onBlur={handleBlur('password')}
                          value={values.password}
                        />
                        <Icon
                          name={showPassword ? 'eye-off' : 'eye'}
                          onPress={() => setShowPassword(!showPassword)}
                        />
                      </Item>
                    </InputGroup>
                    <ValidationMessage>{errors.password}</ValidationMessage>
                  </>
                ) : null}
                <ListItem>
                  <CheckBox
                    checked={values.lockoutEnabled}
                    onPress={() => setFieldValue('lockoutEnabled', !values.lockoutEnabled)}
                  />
                  <Body>
                    <TouchableOpacity
                      onPress={() => setFieldValue('lockoutEnabled', !values.lockoutEnabled)}>
                      <Text>{i18n.t('RocketIdentity::DisplayName:LockoutEnabled')}</Text>
                    </TouchableOpacity>
                  </Body>
                </ListItem>
                <ListItem>
                  <CheckBox
                    checked={values.twoFactorEnabled}
                    onPress={() => setFieldValue('twoFactorEnabled', !values.twoFactorEnabled)}
                  />
                  <Body>
                    <TouchableOpacity
                      onPress={() => setFieldValue('twoFactorEnabled', !values.twoFactorEnabled)}>
                      <Text>{i18n.t('RocketIdentity::DisplayName:TwoFactorEnabled')}</Text>
                    </TouchableOpacity>
                  </Body>
                </ListItem>
              </View>
              <View
                style={{
                  display: selectedTab === 1 ? 'flex' : 'none',
                  flex: 1,
                }}>
                <UserRoles {...{ editingUser, onChangeRoles }} />
              </View>
            </Content>
          </Container>
          <FormButtons
            submit={handleSubmit}
            remove={remove}
            removeMessage={i18n.t('RocketIdentity::UserDeletionConfirmationMessage', {
              0: editingUser.userName,
            })}
            isSubmitDisabled={!isValid}
            isShowRemove={!!editingUser.id && hasRemovePermission}
          />
        </>
      )}
    </Formik>
  );
}

CreateUpdateUserForm.propTypes = {
  editingUser: PropTypes.object,
  submit: PropTypes.func.isRequired,
  remove: PropTypes.func.isRequired,
};

const styles = StyleSheet.create({
  container: {
    marginBottom: 50,
  },
});

export default CreateUpdateUserForm;