using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Weapons 
{
	public class MirrorBlade : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mirror Blade");
			/* Tooltip.SetDefault("The amount of contact damage an enemy does is added to this weapons' damage\n" +
                "You must hit an enemy with the blade to trigger this effect\n" +
                "Consumes mana to fire mirror blasts"); */
		}

		public override void SetDefaults()
		{
			Item.width = 52;
			Item.damage = 236;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.mana = 4;
			Item.useAnimation = 9;
			Item.useTime = 9;
			Item.useTurn = true;
			Item.useStyle = 1;
			Item.knockBack = 8f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 62;
            Item.value = Item.buyPrice(1, 40, 0, 0);
            Item.rare = 10;
            Item.shootSpeed = 16f;
	        Item.shoot = Mod.Find<ModProjectile>("MirrorBlast").Type;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 13;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    	return false;
		}
	    
	    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	    {
	    	int conDamage = target.damage + 236;
	    	if (conDamage < 236)
	    	{
	    		conDamage = 236;
	    	}
            if (conDamage > 500)
            {
                conDamage = 500;
            }
            Item.damage = conDamage;
		}
	}
}
