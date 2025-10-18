using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Entities;
namespace Infrastructure.Services
{
    public class JoueurService(IAuthService authService, IJoueurRepository joueurRepository, IEmailService emailService) : IJoueurService
    {
        private readonly IAuthService _authService = authService;
        private readonly IJoueurRepository _joueurRepository = joueurRepository;
        private readonly IEmailService _emailService = emailService;
        public async Task<Joueur> InscrireJoueurAsync(string nom, string prenom, DateTime dateNaissance, string email, string password, string telephone, List<int> positionIds)
        {
            //validations nom
            if (string.IsNullOrEmpty(nom)) {
                throw new ArgumentException("Le nom ne peut pas être vide.");
            }
            else if (nom.Length < 2 || nom.Length > 50) 
            {
                throw new ArgumentException("Le nom doit contenir entre 2 et 50 caractères.");
            }

            //validations prenom
            if (string.IsNullOrEmpty(prenom))
            {
                throw new ArgumentException("Le prénom ne peut pas être vide.");
            }
            else if (prenom.Length < 2 || prenom.Length > 50)
            {
                throw new ArgumentException("Le prénom doit contenir entre 2 et 50 caractères.");
            }

            //Validations dateNaissance
            if (dateNaissance >= DateTime.Now) {
                throw new ArgumentException("La date de naissance doit être dans le passé.");
            }

            //validations telephone
            var numericTelephone = new string([.. telephone.Where(char.IsDigit)]);

            if (!(numericTelephone is { Length: 10 } || (numericTelephone is { Length: 11 } && numericTelephone.StartsWith("1"))))
            {
                throw new ArgumentException("Le numéro de téléphone doit être au format canadien, par exemple : 4182345678 ou 14182345678.");
            }

            // hash password
            var passwordHash = await _authService.HashPasswordAsync(password);

            // validations email
            if (await _authService.EmailAlreadyUsed(email))
            {
                throw new ArgumentException("L'adresse email est déjà utilisée.");
            }
            var verificationToken = await _authService.GenerateEmailVerificationTokenAsync();

            Joueur joueur = new()
            {
                Nom = nom,
                Prenom = prenom,
                DateNaissance = dateNaissance,
                Email = email,
                Telephone = numericTelephone,
                IsAdmin = false,
                MotDePasseHash = passwordHash,
                IsEmailVerified = false,
                EmailVerificationToken = verificationToken,
                EmailVerificationTokenExpiry = DateTime.UtcNow.AddHours(24)
            };

            await _joueurRepository.AddAsync(joueur);

            await _emailService.SendEmailVerificationAsync(email, nom, prenom, verificationToken);

            return joueur;
        }
    }
}
