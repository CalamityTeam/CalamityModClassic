using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class StormSpray : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/1.DesertScourge/StormSpray");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Storm Spray");
        Item.damage = 15;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 5;
        Item.width = 42;
        Item.height = 42;
        Item.useTime = 23;
        Item.useAnimation = 23;
        Item.useStyle = 5;
        Item.staff[Item.type] = true;
        ////Tooltip.SetDefault("Fires a spray of water that drips extra trails of water");
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 5;
        Item.value = 50000;
        Item.rare = 2;
        Item.UseSound = SoundID.Item13;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("WaterStream").Type;
        Item.shootSpeed = 10f;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
    	Projectile.NewProjectile(source, position, velocity,type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
		return false;
	}
}}