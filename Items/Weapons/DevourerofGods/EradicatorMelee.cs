using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.DevourerofGods
{
    public class EradicatorMelee : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Eradicator");
        }

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.damage = 300;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;
            Item.useAnimation = 19;
            Item.useStyle = 1;
            Item.useTime = 19;
            Item.knockBack = 7f;
            Item.UseSound = SoundID.Item1;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.height = 54;
            Item.value = Item.buyPrice(1, 40, 0, 0);
            Item.rare = 10;
            Item.shoot = Mod.Find<ModProjectile>("EradicatorMeleeProjectile").Type;
            Item.shootSpeed = 12f;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 13;
		}

		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
		{
			Vector2 origin = new Vector2(31f, 29f);
			spriteBatch.Draw(ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/Items/Weapons/DevourerofGods/EradicatorMeleeGlow").Value, Item.Center - Main.screenPosition, null, Color.White, rotation, origin, 1f, SpriteEffects.None, 0f);
		}
	}
}
