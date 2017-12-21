using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using codecampster.ViewModels.Session;
using codecampster.Models;

namespace codecampster.ViewModels.Helpers
{
    public static class SessionViewModelExtensionMethods
    {
        public static SessionViewModel ToSessionViewModel(
            this codecampster.Models.Session session, ApplicationUser applicationUser)
        {
            var viewModel = new SessionViewModel()
            {
                SessionID = session.SessionID,
                Title = session.Name,
                Description = session.Description,
                CoSpeakers = session.CoSpeakers,
                Keywords = session.KeyWords,
                Level = session.Level,
                IsApproved = session.IsApproved,
                Speaker = new Speaker.SpeakerViewModel()
                {
                    ID = session.Speaker.ID,
                    Company = session.Speaker.Company,
                    Title = session.Speaker.Title,
                    Bio = session.Speaker.Bio,
                    Twitter = session.Speaker.Twitter,
                    Website = session.Speaker.Website,
                    Blog = session.Speaker.Blog,
                    AvatarURL = session.Speaker.AvatarURL,
                    MVPDetails = session.Speaker.MVPDetails,
                    AuthorDetails = session.Speaker.AuthorDetails,
                    NoteToOrganizers = session.Speaker.NoteToOrganizers,
                    IsMvp = session.Speaker.IsMvp,
                    PhoneNumber = session.Speaker.PhoneNumber,
                    LinkedIn = session.Speaker.LinkedIn,
                    FullName = applicationUser.FirstName + " " + applicationUser.LastName
                }
            };

            return viewModel;
        }
    }
}
