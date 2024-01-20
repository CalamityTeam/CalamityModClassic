using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class GodSlayerHelm : ModItem
{
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("God Slayer Helm");
        Item.width = 18;
        Item.height = 18;
        ////Tooltip.SetDefault("15% increased damage and critical strike chance\nTaking over 80 damage in one hit will cause you to release a swarm of god killer darts\nAn attack that would deal 80 damage or less will cause the attack to do no damage to you");
        Item.value = 5000000;
        Item.rare = 10;
        Item.defense = 29; //62
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == Mod.Find<ModItem>("GodSlayerChestplate").Type && legs.type == Mod.Find<ModItem>("GodSlayerLeggings").Type;
    }
    
    public override void ArmorSetShadows(Player player)
    {
    	player.armorEffectDrawShadow = true;
    }

    public override void UpdateArmorSet(Player player)
    {
    	CalamityPlayer1Point1 modPlayer = player.GetModPlayer<CalamityPlayer1Point1>();
    	modPlayer.godSlayer = true;
        player.setBonus =("You will survive fatal damage and will be healed 300 HP if an attack would have killed you\nThis effect can only occur once every 30 seconds\nWhile the cooldown for this effect is active you gain a 10% increase to all damage");
    }
    
    public override void UpdateEquip(Player player)
    {
    	CalamityPlayer1Point1 modPlayer = player.GetModPlayer<CalamityPlayer1Point1>();
    	modPlayer.godSlayerDamage = true;
    	player.GetDamage(DamageClass.Melee) *= 1.15f;
        player.GetCritChance(DamageClass.Melee) += 15;
        player.GetDamage(DamageClass.Ranged) *= 1.15f;
        player.GetCritChance(DamageClass.Ranged) += 15;
        player.GetDamage(DamageClass.Magic) *= 1.15f;
        player.GetCritChance(DamageClass.Magic) += 15;
        player.GetDamage(DamageClass.Throwing) *= 1.15f;
        player.GetCritChance(DamageClass.Throwing) += 15;
        player.GetDamage(DamageClass.Summon) *= 1.15f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "CosmiliteBar", 14);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}