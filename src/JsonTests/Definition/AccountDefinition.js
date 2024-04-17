/*
 * Auto generated by XrmFramework 2.0
 * Do not edit directly this file
 */
const accountDefinition = {
    LogicalName: "account",
    SchemaName: "Account",
    CollectionName: "accounts",
    LogicalCollectionName: "accounts",
    PrimaryIdAttribute: "accountid",
    PrimaryNameAttribute: "name",
    Columns: {
        AccountCategoryCode: "accountcategorycode",
        Id: "accountid",
        AccountNumber: "accountnumber",
        Address1_County: "address1_county",
        Address1_Latitude: "address1_latitude",
        Address1_Line3: "address1_line3",
        Address1_PostalCode: "address1_postalcode",
        Address1_StateOrProvince: "address1_stateorprovince",
        Address2_FreightTermsCode: "address2_freighttermscode",
        Name: "name"
    },
    Enums: {
        Categorie: {
            ClientFavori: 1,
            Standard: 2
        },
        Classification: {
            ValeurParDefaut: 1
        },
        EvaluationDeCompte: {
            ValeurParDefaut: 1
        },
        Adresse1TypeDAdresse: {
            Facturation: 1,
            Expedition: 2,
            Principale: 3,
            Autre: 4
        },
        Adresse1ConditionsDeTransport: {
            FAB: 1,
            SansSupplement: 2
        },
        Adresse1ModeDeLivraison: {
            CourrierPostal: 5,
            Collecte: 7,
            Autre: 8,
            Transporteur: 17
        },
        Adresse2TypeDAdresse: {
            ValeurParDefaut: 1
        },
        Adresse2ConditionsDeTransport: {
            ValeurParDefaut: 1
        },
        Adresse2ModeDeLivraison: {
            ValeurParDefaut: 1
        },
        TypeDEntrepriseActivite: {
            ValeurParDefaut: 1
        },
        TailleDuClient: {
            ValeurParDefaut: 1
        },
        TypeDeRelation: {
            Concurrent: 1,
            Consultant: 2,
            Client: 3,
            Investisseur: 4,
            Partenaire: 5,
            Influenceur: 6,
            Presse: 7,
            Prospect: 8,
            Revendeur: 9,
            Fournisseur: 10,
            Fournisseur: 11,
            Autre: 12
        },
        SecteurDActivite: {
            AgricultureChasseForesterieEtPeche: 34,
            EnergieEtEau: 35,
            ExtractionEtTransformationDeMinerauxNonEnergetiquesEtProduitsDerivesIndustrieChimique: 36,
            IndustriesTransformatricesDesMetauxMecaniqueDePrecision: 37,
            AutresIndustriesManufacturieres: 38,
            BatimentEtGenieCivil: 39,
            CommerceRestaurationEtHebergementReparations: 40,
            TransportsEtCommunications: 41,
            InstitutionsDeCreditAssurancesServicesFournisAuxEntreprisesLocation: 42,
            AutresServices: 43
        },
        Propriete: {
            Public: 1,
            Prive: 2,
            Filiale: 3,
            Autre: 4
        },
        ConditionsDePaiement: {
            Net30J: 1,
            Net60J: 4,
            AuComptant: 10,
            AReceptionDeFacture: 15,
            _30JFDM: 16,
            _60JFDM: 17
        },
        JourPrivilegie: {
            Dimanche: 0,
            Lundi: 1,
            Mardi: 2,
            Mercredi: 3,
            Jeudi: 4,
            Vendredi: 5,
            Samedi: 6
        },
        HeurePrivilegiee: {
            Matin: 1,
            ApresMidi: 2,
            Soir: 3
        },
        ModeDeCommunicationPrivilegie: {
            SansPreference: 1,
            CourrierElectronique: 2,
            Telephone: 3,
            Telecopie: 4,
            CourrierPostal: 5
        },
        ModeDeLivraison: {
            ValeurParDefaut: 1
        },
        AccountState: {
            Actif: 0,
            Inactif: 1
        },
        AccountStatus: {
            Actif: 1,
            Inactif: 2
        },
        CodeDeSecteurDeVente: {
            ValeurParDefaut: 1
        }
    }
};