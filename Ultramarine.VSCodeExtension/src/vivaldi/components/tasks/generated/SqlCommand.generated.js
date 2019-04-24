// This file is generated by Ultramarine and should not be modified by hand
import React, { Fragment } from 'react'
import PropTypes from 'prop-types'
import Task from '../task'
import Input from '../../ui/Input'

const SqlCommand = ({
  connectionString,
  statement,
  commandType,
  queryType,
  editable,
  onChange,
  ...rest
}) => (
  <Task editable={editable} {...rest} icon={SqlCommand.icon}>
    {editable ? (
      <Fragment>
		<Input
          id="connectionString"
          text="Connection String"
          value={connectionString}
          onChange={onChange}
        />
		<Input
          id="statement"
          text="Statement"
          value={statement}
          onChange={onChange}
        />
		<Input
          id="commandType"
          text="Command Type"
          value={commandType}
          onChange={onChange}
        />
		<Input
          id="queryType"
          text="Query Type"
          value={queryType}
          onChange={onChange}
        />
        
      </Fragment>
    ) : (
      <Fragment>
		<p>Connection String: {connectionString}</p>
		<p>Statement: {statement}</p>
		<p>Command Type: {commandType}</p>
		<p>Query Type: {queryType}</p>
      </Fragment>
    )}
  </Task>
)

SqlCommand.description = 'Sql Command'
SqlCommand.icon = 'code'
SqlCommand.type = 'sqlCommand'

SqlCommand.propTypes = {
  connectionString: PropTypes.string,
  statement: PropTypes.string,
  commandType: PropTypes.string,
  queryType: PropTypes.string,
  editable: PropTypes.bool,
  onChange: PropTypes.func
}

SqlCommand.defaultProps = {
  connectionString: '',
  statement: '',
  commandType: '',
  queryType: '',
  editable: false,
  onChange: () => {}
}

export default SqlCommand
