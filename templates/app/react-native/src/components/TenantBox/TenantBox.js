import i18n from 'i18n-js';
import {
  Button,
  connectStyle,
  Content,
  Input,
  InputGroup,
  Label,
  Segment,
  Text,
} from 'native-base';
import PropTypes from 'prop-types';
import React, { forwardRef, useState } from 'react';
import { StyleSheet, View, Alert } from 'react-native';
import { getTenant } from '../../api/AccountAPI';
import PersistentStorageActions from '../../store/actions/PersistentStorageActions';
import { connectToRedux } from '../../utils/ReduxConnect';
import { createTenantSelector } from '../../store/selectors/PersistentStorageSelectors';

function TenantBox({ style, tenant = {}, setTenant, showTenantSelection, toggleTenantSelection }) {
  const [tenantName, setTenantName] = useState(tenant.name);

  const findTenant = () => {
    if (!tenantName) {
      setTenant({});
      toggleTenantSelection();
      return;
    }

    getTenant(tenantName).then(({ success, ...data }) => {
      if (!success) {
        Alert.alert(
          i18n.t('RocketUi::Error'),
          i18n.t('RocketUiMultiTenancy::GivenTenantIsNotAvailable', {
            0: tenantName,
          }),
          [{ text: i18n.t('RocketUi::Ok') }],
        );
        return;
      }
      setTenant(data);
      toggleTenantSelection();
    });
  };

  return (
    <>
      <Segment style={style.container}>
        <View>
          <Text style={style.title}>{i18n.t('RocketUiMultiTenancy::Tenant')}</Text>
          <Text style={style.tenant}>
            {tenant.name ? tenant.name : i18n.t('RocketUiMultiTenancy::NotSelected')}
          </Text>
        </View>
        <Button
          style={{ ...style.switchButton, display: !showTenantSelection ? 'flex' : 'none' }}
          onPress={() => toggleTenantSelection()}>
          <Text style={{ color: '#fff' }}>{i18n.t('RocketUiMultiTenancy::Switch')}</Text>
        </Button>
      </Segment>
      {showTenantSelection ? (
        <Content px20 style={{ flex: 1 }}>
          <InputGroup rocketInputGroup>
            <Label rocketLabel>{i18n.t('RocketUiMultiTenancy::Name')}</Label>
            <Input rocketInput value={tenantName} onChangeText={setTenantName} />
          </InputGroup>
          <Text style={style.hint}>{i18n.t('RocketUiMultiTenancy::SwitchTenantHint')}</Text>
          <View style={{ flexDirection: 'row', justifyContent: 'space-between' }}>
            <Button rocketButton light style={style.button} onPress={() => toggleTenantSelection()}>
              <Text>{i18n.t('RocketAccount::Cancel')}</Text>
            </Button>
            <Button rocketButton style={style.button} onPress={() => findTenant()}>
              <Text>{i18n.t('RocketAccount::Save')}</Text>
            </Button>
          </View>
        </Content>
      ) : null}
    </>
  );
}

TenantBox.propTypes = {
  style: PropTypes.any.isRequired,
  setTenant: PropTypes.func.isRequired,
  showTenantSelection: PropTypes.bool.isRequired,
  toggleTenantSelection: PropTypes.func.isRequired,
  tenant: PropTypes.object.isRequired,
};

const styles = StyleSheet.create({
  container: {
    paddingHorizontal: 20,
    alignItems: 'center',
    justifyContent: 'space-between',
    height: 70,
  },
  button: { marginTop: 20, width: '49%' },
  switchButton: {
    borderTopWidth: 0,
    borderRightWidth: 0,
    borderBottomWidth: 0,
    borderLeftWidth: 0,
    borderRadius: 10,
    backgroundColor: '#38003c',
    height: 35,
  },
  tenant: { color: '#777' },
  title: {
    marginRight: 10,
    color: '#777',
    fontSize: 13,
    fontWeight: '600',
    textTransform: 'uppercase',
  },
  hint: { color: '#bbb', textAlign: 'left' },
});

const Forwarded = forwardRef((props, ref) => <TenantBox {...props} forwardedRef={ref} />);

export default connectToRedux({
  component: connectStyle('ABP.TenantBox', styles)(Forwarded),
  dispatchProps: {
    setTenant: PersistentStorageActions.setTenant,
  },
  stateProps: state => ({
    tenant: createTenantSelector()(state),
  }),
});
