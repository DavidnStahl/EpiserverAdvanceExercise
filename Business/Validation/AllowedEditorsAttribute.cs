
using EPiServer.Cms.Shell.UI.ObjectEditing;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace AlloyAdvance.Business.Validation
{
    public class AllowedEditorsAttribute : ValidationAttribute, IMetadataAware
    {
        public string[] Roles { get; set; }
        public NotAllowedAction NotAllowedAction { get; set; }

        public void OnMetadataCreated(ModelMetadata metadata)
        {
            var contentMetadata = metadata as ContentDataMetadata;

            if (contentMetadata.InitialValue != null && NotAllowedAction == NotAllowedAction.Readonly)
                CheckReadonly(contentMetadata);

            if (contentMetadata.InitialValue != null && NotAllowedAction == NotAllowedAction.Hidden)
                CheckHidden(contentMetadata);
        }

        private void CheckReadonly(ContentDataMetadata metadata)
        {
            foreach (var role in Roles)
            {
                bool currentUserIsInRole = HttpContext.Current.User.IsInRole(role);

                if (currentUserIsInRole)
                {
                    metadata.IsReadOnly = false;
                }
            }

            return;
        }

        private void CheckHidden(ContentDataMetadata metadata)
        {
            foreach (var role in Roles)
            {
                bool currentUserIsInRole = HttpContext.Current.User.IsInRole(role);

                if (currentUserIsInRole)
                {
                    metadata.ShowForEdit = false;
                }
            }

            return;
        }
    }

    public enum NotAllowedAction
    {
        Readonly,
        Hidden
    }
}