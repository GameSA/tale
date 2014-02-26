<?php
require dirname(__FILE__).'/lib/adLDAP.php';

try {
    $adldap = new adLDAP(
    	array(
    		'base_dn' 			=> 'dc=intranet,dc=123u,dc=com',
            'account_suffix'	=> '@intranet.123u.com',
            'domain_controllers' => array('xx.xx.xx.xx'),
            'admin_username'	=> 'xx',
            'admin_password'	=> 'xxxx',
    	)
    );
}
catch (adLDAPException $e) {
    die($e);
}

// 用户信息搜索
if (isset($_POST['keyword'])) {
	$keyword = trim($_POST['keyword']);
	if (strlen($keyword) && preg_match("/^[\x{4e00}-\x{9fa5}a-zA-Z]+$/u", $keyword)) {
		die(ldap_user_search($keyword));
	} else {
		die('input invalid.');
	}
}

// 用户验证
if (isset($_POST['u']) && isset($_POST['p'])) {
	$user = trim($_POST['u']);
	$pass = trim($_POST['p']);

	$ret = $adldap->authenticate($user, $pass);
	$adldap->close();

	die(json_encode(array("auth" => $ret)));
}

// 为微信提供员工信息搜索。adLDAP不支持这样的查询。
function ldap_user_search($keyword) {
	global $adldap;

	$filter = "(|(displayname=*$keyword*)(samaccountname=*$keyword*))";
	$base = 'OU=users,OU=123u,DC=intranet,DC=123u,DC=com';
	$base_array = explode(',', $base);
	$searcher = ldap_search($adldap->getLdapConnection(), $base, $filter, array('telephonenumber', 'cn', 'displayname'), 0);
	$entries = ldap_get_entries($adldap->getLdapConnection(), $searcher);

	$users = array();
	foreach($entries as $entry) {
		if (is_array($entry) && isset($entry['dn'])) {
			$dn_trunk = ldap_explode_dn($entry['dn'], 0);
			$dept_array = array_diff($dn_trunk, $base_array);

			array_shift($dept_array);
			array_shift($dept_array);

			$dept_name = '';
			foreach (array_reverse($dept_array) as $dept) {
				// 部门有中文的返回真坑爹
				$dept_name .= '-' . preg_replace_callback(
					"/\\\([0-9A-Fa-f]{2})/",
					function ($match) {return chr(hexdec($match[1]));},
					str_replace('OU=', '', $dept)
				);
			}
			$dept_name = substr($dept_name, 1);

			$users[] = array(
				'displayname' => $entry['displayname'][0],
				'department' => $dept_name,
				'telephonenumber' => $entry['telephonenumber'][0],
			);
		}
	}

	$adldap->close();
	return json_encode(($users));
}

