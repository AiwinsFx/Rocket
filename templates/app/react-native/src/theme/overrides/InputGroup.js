// @flow
import inputGroupTheme from '../components/InputGroup';

export default variables => {
  const inputGroupOverrides = {
    ...inputGroupTheme(variables),
    '.rocketInputGroup': {
      display: 'flex',
      flexDirection: 'column',
      justifyContent: 'center',
      padding: 0,
      marginVertical: 10,
      borderBottomWidth: 0,
      height: 60,
    },
  };

  return inputGroupOverrides;
};
