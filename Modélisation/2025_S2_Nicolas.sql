/*==============================================================*/
/* Nom de SGBD :  PostgreSQL 8                                  */
/* Date de création :  05/06/2025 15:27:19                      */
/*==============================================================*/


drop table if exists APPELATION cascade ;

drop table if exists CLIENT cascade ;

drop table if exists COMMANDE cascade  ;

drop table if exists DEMANDE cascade  ;

drop table if exists DETAILCOMMANDE cascade  ;

drop table if exists EMPLOYE cascade ;

drop table if exists FOURNISSEUR cascade ;

drop table if exists ROLE cascade ;

drop table if exists TYPEVIN cascade ;

drop table if exists VIN cascade ;

/*==============================================================*/
/* Table : APPELATION                                           */
/*==============================================================*/
create table APPELATION (
   NUMAPPELATION             SERIAL               not null,
   NOMAPPELATION        VARCHAR(50)          null,
   constraint PK_APPELATION primary key (NUMAPPELATION)
);

/*==============================================================*/
/* Table : CLIENT                                               */
/*==============================================================*/
create table CLIENT (
   NUMCLIENT            SERIAL               not null,
   NOMCLIENT            VARCHAR(50)          null,
   PRENOMCLIENT         VARCHAR(50)          null,
   MAILCLIENT           VARCHAR(100)         null,
   constraint PK_CLIENT primary key (NUMCLIENT)
);

/*==============================================================*/
/* Table : COMMANDE                                             */
/*==============================================================*/
create table COMMANDE (
   NUMCOMMANDE          SERIAL               not null,
   NUMEMPLOYE           INT4                 not null,
   DATECOMMANDE         DATE                 null,
   ETATCOMMANDE         VARCHAR(50)          null,
   PRIXTOTAL            DECIMAL(16,2)         null,
   constraint PK_COMMANDE primary key (NUMCOMMANDE)
);

/*==============================================================*/
/* Table : DEMANDE                                              */
/*==============================================================*/
create table DEMANDE (
   NUMDEMANDE           SERIAL               not null,
   NUMVIN               INT4                 not null,
   NUMEMPLOYE           INT4                 not null,
   NUMCOMMANDE          INT4                 null,
   NUMCLIENT            INT4                 not null,
   DATEDEMANDE          DATE                 null,
   QUANTITEDEMANDE      INT4                 null,
   ETATDEMANDE          VARCHAR(50)          null,
   constraint PK_DEMANDE primary key (NUMDEMANDE)
);

/*==============================================================*/
/* Table : DETAILCOMMANDE                                       */
/*==============================================================*/
create table DETAILCOMMANDE (
   NUMCOMMANDE          INT4                 not null,
   NUMVIN               INT4                 not null,
   QUANTITE             INT4                 null
      constraint CKC_QUANTITE_DETAILCO check (QUANTITE is null or (QUANTITE between 1 and 100)),
   PRIX                 DECIMAL(16,2)         null,
   constraint PK_DETAILCOMMANDE primary key (NUMCOMMANDE, NUMVIN)
);

/*==============================================================*/
/* Table : EMPLOYE                                              */
/*==============================================================*/
create table EMPLOYE (
   NUMEMPLOYE           SERIAL               not null,
   NUMROLE              INT4                 not null,
   NOM                  VARCHAR(50)          null,
   PRENOM               VARCHAR(50)          null,
   LOGIN                VARCHAR(50)          null,
   MDP                  VARCHAR(50)          null,
   constraint PK_EMPLOYE primary key (NUMEMPLOYE)
);

/*==============================================================*/
/* Table : FOURNISSEUR                                          */
/*==============================================================*/
create table FOURNISSEUR (
   NUMFOURNISSEUR       SERIAL               not null,
   NOMFOURNISSEUR       VARCHAR(50)          null,
   constraint PK_FOURNISSEUR primary key (NUMFOURNISSEUR)
);

/*==============================================================*/
/* Table : ROLE                                                 */
/*==============================================================*/
create table ROLE (
   NUMROLE              SERIAL               not null,
   NOMROLE              VARCHAR(50)          null,
   constraint PK_ROLE primary key (NUMROLE)
);

/*==============================================================*/
/* Table : TYPEVIN                                              */
/*==============================================================*/
create table TYPEVIN (
   NUMTYPE              SERIAL               not null,
   NOMTYPE              VARCHAR(50)          null,
   constraint PK_TYPEVIN primary key (NUMTYPE)
);

/*==============================================================*/
/* Table : VIN                                                  */
/*==============================================================*/
create table VIN (
   NUMVIN               SERIAL               not null,
   NUMFOURNISSEUR       INT4                 not null,
   NUMTYPE              INT4                 not null,
   NUMAPPELATION        INT4                 not null,
   NOMVIN               VARCHAR(100)         null,
   PRIXVIN              DECIMAL(16,2)        null,
   DESCRIPTIF           VARCHAR(300)         null,
   MILLESIME            INT4                 null,
   constraint PK_VIN primary key (NUMVIN)
);

alter table COMMANDE
   add constraint FK_COMMANDE_REALISE_EMPLOYE foreign key (NUMEMPLOYE)
      references EMPLOYE (NUMEMPLOYE)
      on delete restrict on update restrict;

alter table DEMANDE
   add constraint FK_DEMANDE_CONCERNE_VIN foreign key (NUMVIN)
      references VIN (NUMVIN)
      on delete restrict on update restrict;

alter table DEMANDE
   add constraint FK_DEMANDE_ENREGISTR_EMPLOYE foreign key (NUMEMPLOYE)
      references EMPLOYE (NUMEMPLOYE)
      on delete restrict on update restrict;

alter table DEMANDE
   add constraint FK_DEMANDE_FAIRE_CLIENT foreign key (NUMCLIENT)
      references CLIENT (NUMCLIENT)
      on delete restrict on update restrict;

alter table DEMANDE
   add constraint FK_DEMANDE_LIENDDECD_COMMANDE foreign key (NUMCOMMANDE)
      references COMMANDE (NUMCOMMANDE)
      on delete restrict on update restrict;

alter table DETAILCOMMANDE
   add constraint FK_DETAILCO_DETAILCOM_COMMANDE foreign key (NUMCOMMANDE)
      references COMMANDE (NUMCOMMANDE)
      on delete restrict on update restrict;

alter table DETAILCOMMANDE
   add constraint FK_DETAILCO_DETAILCOM_VIN foreign key (NUMVIN)
      references VIN (NUMVIN)
      on delete restrict on update restrict;

alter table EMPLOYE
   add constraint FK_EMPLOYE_LIE_ROLE foreign key (NUMROLE)
      references ROLE (NUMROLE)
      on delete restrict on update restrict;

alter table VIN
   add constraint FK_VIN_APPARTIEN_APPELATI foreign key (NUMAPPELATION)
      references APPELATION (NUMAPPELATION)
      on delete restrict on update restrict;

alter table VIN
   add constraint FK_VIN_CARACTERI_TYPEVIN foreign key (NUMTYPE)
      references TYPEVIN (NUMTYPE)
      on delete restrict on update restrict;

alter table VIN
   add constraint FK_VIN_FOURNIT_FOURNISS foreign key (NUMFOURNISSEUR)
      references FOURNISSEUR (NUMFOURNISSEUR)
      on delete restrict on update restrict;
