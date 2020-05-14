import React, { forwardRef } from 'react';
import PropTypes from 'prop-types';
import { Button, Text, connectStyle } from 'native-base';
import { View, StyleSheet, Alert } from 'react-native';
import i18n from 'i18n-js';

function FormButtons({
  style,
  submit,
  remove,
  removeMessage,
  isRemoveDisabled,
  isSubmitDisabled,
  isShowRemove = false,
  isShowSubmit = true,
}) {
  const confirmation = () => {
    Alert.alert(
      i18n.t('RocketUi::AreYouSure'),
      removeMessage,
      [
        {
          text: i18n.t('RocketUi::Cancel'),
          style: 'cancel',
        },
        { text: i18n.t('RocketUi::Yes'), onPress: () => remove() },
      ],
      { cancelable: true },
    );
  };

  return (
    <View style={style.container}>
      {isShowRemove ? (
        <Button
          rocketButton
          danger
          style={{ flex: 1, borderRadius: 0 }}
          onPress={() => confirmation()}
          disabled={isRemoveDisabled}>
          <Text>{i18n.t('RocketIdentity::Delete')}</Text>
        </Button>
      ) : null}
      {isShowSubmit ? (
        <Button
          rocketButton
          primary
          style={{ flex: 1, borderRadius: 0 }}
          onPress={submit}
          disabled={isSubmitDisabled}>
          <Text>{i18n.t('RocketIdentity::Save')}</Text>
        </Button>
      ) : null}
    </View>
  );
}

FormButtons.propTypes = {
  submit: PropTypes.func.isRequired,
  remove: PropTypes.func,
  removeMessage: PropTypes.string,
  style: PropTypes.any,
  isRemoveDisabled: PropTypes.bool,
  isSubmitDisabled: PropTypes.bool,
  isShowRemove: PropTypes.bool,
  isShowSubmit: PropTypes.bool,
};

const styles = StyleSheet.create({
  container: {
    width: '100%',
    justifyContent: 'center',
    alignItems: 'center',
    position: 'absolute',
    bottom: 0,
    flexDirection: 'row',
  },
});

const Forwarded = forwardRef((props, ref) => <FormButtons {...props} forwardedRef={ref} />);

export default connectStyle('ROCKET.FormButtons', styles)(Forwarded);
