import React from 'react';
import { createStackNavigator } from '@react-navigation/stack';
import UsersScreen from '../screens/Users/UsersScreen';
import CreateUpdateUserScreen from '../screens/CreateUpdateUser/CreateUpdateUserScreen';
import MenuIcon from '../components/MenuIcon/MenuIcon';
import { LocalizationContext } from '../contexts/LocalizationContext';

const Stack = createStackNavigator();

export default function UsersStackNavigator() {
  const { t } = React.useContext(LocalizationContext);

  return (
    <Stack.Navigator initialRouteName="Users">
      <Stack.Screen
        name="Users"
        component={UsersScreen}
        options={({ navigation }) => ({
          headerLeft: () => <MenuIcon onPress={() => navigation.openDrawer()} />,
          title: t('RocketIdentity::Users'),
        })}
      />
      <Stack.Screen
        name="CreateUpdateUser"
        component={CreateUpdateUserScreen}
        options={({ route }) => ({
          title: t(route.params?.userId ? 'RocketIdentity::Edit' : 'RocketIdentity::NewUser'),
        })}
      />
    </Stack.Navigator>
  );
}
