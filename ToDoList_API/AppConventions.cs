using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using ToDoList_API.Errors;

#nullable disable
#pragma warning disable IDE0060 // Remove unused parameter

namespace ToDoList_API
{
    public static class AppConventions
    {
        #region GET
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UnauthorizedError), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(NotFoundError), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType(typeof(InternalServerError))]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void GetAll(
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix)]
            [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
            params object[] id)
        { }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UnauthorizedError), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(NotFoundError), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType(typeof(InternalServerError))]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Get(
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix)]
            [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
             params object[] id)
        { }
        #endregion

        #region POST
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BadRequestError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UnauthorizedError), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(NotFoundError), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType(typeof(InternalServerError))]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Create(
            [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
            params object[] paramObjects)
        { }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(BadRequestError), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType(typeof(InternalServerError))]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Exact)]
        public static void Register(
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix)]
            [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
            object dto)
        { }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UnauthorizedError), StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType(typeof(InternalServerError))]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Exact)]
        public static void Login(
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix)]
            [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
            object dto)
        { }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestError), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType(typeof(InternalServerError))]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Exact)]
        public static void RefreshToken(
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix)]
            [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
            object dto)
        { }
        #endregion

        #region PUT
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(BadRequestError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UnauthorizedError), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(NotFoundError), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType(typeof(InternalServerError))]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Put(
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix)]
            [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
            object dto)
        { }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(BadRequestError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UnauthorizedError), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(NotFoundError), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType(typeof(InternalServerError))]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Update(
            [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
            params object[] paramObjects)
        { }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(BadRequestError), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType(typeof(InternalServerError))]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void ForgotPassword(
            [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
            params object[] paramObjects)
        { }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(BadRequestError), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType(typeof(InternalServerError))]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void ResetPassword(
            [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
            params object[] paramObjects)
        { }
        #endregion

        #region DELETE
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(BadRequestError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UnauthorizedError), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(NotFoundError), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType(typeof(InternalServerError))]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Delete(
            [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
            params object[] paramObjects)
        { }
        #endregion
    }
}

#pragma warning restore IDE0060 // Remove unused parameter
