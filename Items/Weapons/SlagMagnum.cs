using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class SlagMagnum : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/SlagMagnum");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Slag Magnum");
        Item.damage = 30;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 76;
        Item.height = 22;
        ////Tooltip.SetDefault("Fires an earth bullet that breaks into shards and shatters enemy defense");
        Item.useTime = 15;
        Item.useAnimation = 15;
        Item.useStyle = 5;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 4f;
        Item.value = 100000;
        Item.rare = 5;
        Item.UseSound = SoundID.Item41;
        Item.autoReuse = true;
        Item.shootSpeed = 22f;
        Item.shoot = Mod.Find<ModProjectile>("SlagRound").Type;
        Item.useAmmo = 97;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	Projectile.NewProjectile(source, position, velocity,Mod.Find<ModProjectile>("SlagRound").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
    	return false;
	}
}}