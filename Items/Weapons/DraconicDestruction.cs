using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Weapons 
{
	public class DraconicDestruction : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Draconic Destruction");
			// Tooltip.SetDefault("Fires a draconic sword beam that explodes into additional beams\nAdditional beams fly up and down to shred enemies");
		}

		public override void SetDefaults()
		{
			Item.width = 94;
			Item.damage = 210;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 24;
			Item.useStyle = 1;
			Item.useTime = 24;
			Item.useTurn = true;
			Item.knockBack = 7.25f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 94;
            Item.value = Item.buyPrice(5, 0, 0, 0);
            Item.rare = 10;
            Item.shoot = Mod.Find<ModProjectile>("DracoBeam").Type;
			Item.shootSpeed = 14f;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 16;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "ShadowspecBar", 5);
			recipe.AddIngredient(null, "CoreofCinder", 3);
			recipe.AddIngredient(null, "CoreofEleum", 3);
			recipe.AddIngredient(ItemID.FragmentSolar, 10);
	        recipe.AddTile(null, "DraedonsForge");
	        recipe.Register();
		}
	
	    public override void MeleeEffects(Player player, Rectangle hitbox)
	    {
	        if (Main.rand.Next(5) == 0)
	        {
	        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 35);
	        }
	    }
	    
	    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
	    	target.AddBuff(BuffID.OnFire, 500);
		}
	}
}
